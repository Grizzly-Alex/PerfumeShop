namespace Microsoft.eShopWeb.Web.Areas.Identity.Pages;

[AllowAnonymous]
public class RegisterModel : PageModel
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<RegisterModel> _logger;
    private readonly IEmailService _emailService;

    public RegisterModel(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ILogger<RegisterModel> logger,
        IEmailService emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _emailService = emailService;
    }

    [BindProperty]
    public InputModel? Input { get; set; }

    public string? ReturnUrl { get; set; }

    public class InputModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Street Address")]
        public string? StreetAddress { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "City")]
        public string? City { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "State")]
        public string? State { get; set; }

        [DataType(DataType.PostalCode)]
        [Display(Name = "Postal Code")]
        public string? PostalCode { get; set; }

        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }

    public void OnGet(string? returnUrl = null)
    {
        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        if (ModelState.IsValid)
        {
            var user = new AppUser
            {
                UserName = Input.Email,
                FirstName = Input.FirstName,
                LastName = Input.LastName,
                StreetAddress = Input.StreetAddress,
                City = Input.City,
                State = Input.State,
                PostalCode = Input.PostalCode,
                PhoneNumber = Input.PhoneNumber,
                Email = Input.Email,               
            };
            var result = await _userManager.CreateAsync(user, Input.Password);            
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                await _userManager.AddToRoleAsync(user, Roles.Customer.ToString());

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Page(
                    "./ConfirmEmail",
                    pageHandler: null,
                    values: new { userId = user.Id, code = code },
                    protocol: Request.Scheme);

                await _emailService.SendEmailConfirmationAsync(
                    new ConfirmationEmailViewModel
                    {
                        Email = Input.Email,
                        ConfirmationLink = HtmlEncoder.Default.Encode(callbackUrl)
                    });

                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl);
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return Page();
    }
}
