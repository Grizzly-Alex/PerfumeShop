using System.Text;
using Microsoft.AspNetCore.WebUtilities;


namespace PerfumeShop.Web.Areas.Identity.Pages
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserStore<AppUser> _userStore;
        private readonly IUserEmailStore<AppUser> _emailStore;
        private readonly ILogger<ExternalLoginModel> _logger;
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public ExternalLoginModel(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IUserStore<AppUser> userStore,
            ILogger<ExternalLoginModel> logger,
            IBasketService basketService,
            IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _logger = logger;
            _basketService = basketService;
            _mapper = mapper;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public AssociateExternalProviderViewModel? Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ProviderDisplayName { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>


        public IActionResult OnGet() => RedirectToPage("./Login");

        public IActionResult OnPost(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl ??= Url.Content("~/");
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);

                await TransferAnonymousBasketToUserAsync(info.Principal.FindFirstValue(ClaimTypes.Email));
                return LocalRedirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ReturnUrl = returnUrl;
                ProviderDisplayName = info.ProviderDisplayName;

                CreateInputModel(info);

                return Page();
            }
        }

        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (ModelState.IsValid)
            {
                var user = _mapper.Map<AppUser>(Input);

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Customer.ToString());

                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);

                        if (_userManager.Options.SignIn.RequireConfirmedEmail)
                        {
                            return RedirectToPage("./RegisterConfirmation", new { Input.Email });
                        }

                        await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ProviderDisplayName = info.ProviderDisplayName;
            ReturnUrl = returnUrl;
            return Page();
        }

        private void CreateInputModel(ExternalLoginInfo info)
        {
            Input = new AssociateExternalProviderViewModel();

            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                Input.Email = info.Principal.FindFirstValue(ClaimTypes.Email);
            }
            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Country))
            {
                Input.State = info.Principal.FindFirstValue(ClaimTypes.Country);
            }
            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.GivenName))
            {
                Input.FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName);
            }
            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Surname))
            {
                Input.LastName = info.Principal.FindFirstValue(ClaimTypes.Surname);
            }
            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.PostalCode))
            {
                Input.PostalCode = info.Principal.FindFirstValue(ClaimTypes.PostalCode);
            }
            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.MobilePhone))
            {
                Input.PhoneNumber = info.Principal.FindFirstValue(ClaimTypes.MobilePhone);
            }
            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.StreetAddress))
            {
                Input.StreetAddress = info.Principal.FindFirstValue(ClaimTypes.StreetAddress);
            }
        }

        private IUserEmailStore<AppUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<AppUser>)_userStore;
        }

        private async Task TransferAnonymousBasketToUserAsync(string? userName)
        {
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIE))
            {
                var anonymousId = Request.Cookies[Constants.BASKET_COOKIE];

                if (Guid.TryParse(anonymousId, out var _))
                {
                    await _basketService.TransferBasketAsync(anonymousId, userName);
                }

                Response.Cookies.Delete(Constants.BASKET_COOKIE);
            }
            HttpContext.Session.Remove(Constants.BASKET_ITEMS_QTY);
        }
    }
}
