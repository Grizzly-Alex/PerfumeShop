namespace PerfumeShop.Web.Areas.Identity.Controllers;

[Authorize]
[Area("Identity")]
[Route("[controller]/[action]")]
[ApiExplorerSettings(IgnoreApi = true)]
public class AccountManageController : Controller
{
    [TempData]
    public string? NotifText { get; set; }
    [TempData]
    public NotificationStatus? NotifStatus { get; set; }

    private readonly ILogger<AccountManageController> _logger;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IMapper _mapper;


    public AccountManageController(
        ILogger<AccountManageController> logger,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
		IMapper mapper)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> MyProfile()
    {
        var user = await _userManager.GetUserAsync(User)
            ?? throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        var userViewModel = _mapper.Map<IndexUserViewModel>(user);
        userViewModel.Notification = new(NotifStatus, NotifText);

        return View(userViewModel);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MyProfile(IndexUserViewModel userViewModel)
    {
        if (!ModelState.IsValid) return View(userViewModel);

        var user = await _userManager.GetUserAsync(User)
            ?? throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        bool isUpdated = false;

        #region Set Properties
        if (userViewModel.Email != user.Email)
        {
            var setEmailResult = await _userManager.SetEmailAsync(user, userViewModel.Email);
            if (!setEmailResult.Succeeded)
                throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
            isUpdated = true;
        }

        if (userViewModel.PhoneNumber != user.PhoneNumber)
        {
            var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, userViewModel.PhoneNumber);
            if (!setPhoneResult.Succeeded)
                throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
            isUpdated = true;
        }

        if (userViewModel.FirstName != user.FirstName)
        {
            user.FirstName = userViewModel.FirstName;
            var setFirstNameResult = await _userManager.UpdateAsync(user);
            if (!setFirstNameResult.Succeeded)
                throw new ApplicationException($"Unexpected error occurred setting first name for user with ID '{user.Id}'.");
            isUpdated = true;
        }

        if (userViewModel.LastName != user.LastName)
        {
            user.LastName = userViewModel.LastName;
            var setLastNameResult = await _userManager.UpdateAsync(user);
            if (!setLastNameResult.Succeeded)
                throw new ApplicationException($"Unexpected error occurred setting last name for user with ID '{user.Id}'.");
            isUpdated = true;
        }

        if (userViewModel.State != user.State)
        {
            user.State = userViewModel.State;
            var setStateResult = await _userManager.UpdateAsync(user);
            if (!setStateResult.Succeeded)
                throw new ApplicationException($"Unexpected error occurred setting city for user with ID '{user.Id}'.");
            isUpdated = true;
        }

        if (userViewModel.City != user.City)
        {
            user.City = userViewModel.City;
            var setCityResult = await _userManager.UpdateAsync(user);
            if (!setCityResult.Succeeded)
                throw new ApplicationException($"Unexpected error occurred setting city for user with ID '{user.Id}'.");
            isUpdated = true;
        }

        if (userViewModel.StreetAddress != user.StreetAddress)
        {
            user.StreetAddress = userViewModel.StreetAddress;
            var setStreetAddressResult = await _userManager.UpdateAsync(user);
            if (!setStreetAddressResult.Succeeded)
                throw new ApplicationException($"Unexpected error occurred setting street address for user with ID '{user.Id}'.");
            isUpdated = true;
        }

        if (userViewModel.PostalCode != user.PostalCode)
        {
            user.PostalCode = userViewModel.PostalCode;
            var setPostalCodeResult = await _userManager.UpdateAsync(user);
            if (!setPostalCodeResult.Succeeded)
                throw new ApplicationException($"Unexpected error occurred setting postal code for user with ID '{user.Id}'.");
            isUpdated = true;
        }
        #endregion

        if (isUpdated)
        {
            NotifStatus = NotificationStatus.Success;
            NotifText = "Your profile has been updated";
        };

        return RedirectToAction(nameof(MyProfile));
    }

    [HttpGet]
    public async Task<IActionResult> SetPassword()
    {
        var user = await _userManager.GetUserAsync(User)
            ?? throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        var hasPassword = await _userManager.HasPasswordAsync(user);

        if (hasPassword) return RedirectToAction(nameof(ChangePassword));

        var viewModel = new SetPasswordViewModel 
        {
            Notification = new(NotifStatus, NotifText),
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = await _userManager.GetUserAsync(User)
            ?? throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
        if (!addPasswordResult.Succeeded)
        {
            AddErrors(addPasswordResult);
            return View(model);
        }

        await _signInManager.SignInAsync(user, isPersistent: false);

        NotifStatus = NotificationStatus.Success;
        NotifText = "Your password has been set.";

        return RedirectToAction(nameof(SetPassword));
    }

    [HttpGet]
    public async Task<IActionResult> ChangePassword()
    {
        var user = await _userManager.GetUserAsync(User)
            ?? throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        var hasPassword = await _userManager.HasPasswordAsync(user);

        if (!hasPassword) return RedirectToAction(nameof(SetPassword));

        var viewModel = new ChangePasswordViewModel
        {
            Notification = new(NotifStatus, NotifText)
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = await _userManager.GetUserAsync(User)
            ?? throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        if (!changePasswordResult.Succeeded)
        {
            AddErrors(changePasswordResult);
            return View(model);
        }

        await _signInManager.SignInAsync(user, isPersistent: false);
        _logger.LogInformation("User changed their password successfully.");
        NotifText = "Your password has been changed.";

        return RedirectToAction(nameof(ChangePassword));
    }

    private void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}
