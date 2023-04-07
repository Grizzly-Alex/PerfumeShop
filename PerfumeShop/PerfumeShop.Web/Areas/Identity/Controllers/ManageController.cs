using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using PerfumeShop.Web.ViewModels.UserManage;

namespace PerfumeShop.Web.Areas.Identity.Controllers;

[Authorize]
[Area("Identity")]
[Route("[controller]/[action]")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ManageController : Controller
{
    [TempData]
    public string? StatusMessage { get; set; }

    private readonly ILogger<ManageController> _logger;
    private readonly IEmailSender _emailSender;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UrlEncoder _urlEncoder;

    private const string RecoveryCodesKey = nameof(RecoveryCodesKey);
    private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

    public ManageController(
        ILogger<ManageController> logger,
        IEmailSender emailSender,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        UrlEncoder urlEncoder)
    {
        _logger = logger;
        _emailSender = emailSender;
        _userManager = userManager;
        _signInManager = signInManager;
        _urlEncoder = urlEncoder;
    }

    [HttpGet]
    public async Task<IActionResult> MyAccount()
    {
        var user = await _userManager.GetUserAsync(User)
            ?? throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        var userViewModel = new IndexUserViewModel
        {
            UserName = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            StreetAddress = user.StreetAddress,
            City = user.City,
            State = user.State,
            PostalCode = user.PostalCode,
            PhoneNumber = user.PhoneNumber,
            IsEmailConfirmed = user.EmailConfirmed,
            StatusMessage = StatusMessage
        };

        return View(userViewModel);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MyAccount(IndexUserViewModel userViewModel)
    {
        if (!ModelState.IsValid) return View(userViewModel);

        var user = await _userManager.GetUserAsync(User)
            ?? throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        if (userViewModel.Email != user.Email)
        {
            var setEmailResult = await _userManager.SetEmailAsync(user, userViewModel.Email);
            if (!setEmailResult.Succeeded)
                throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
        }

        if (userViewModel.PhoneNumber != user.PhoneNumber)
        {
            var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, userViewModel.PhoneNumber);
            if (!setPhoneResult.Succeeded)
                throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
        }

        if (userViewModel.FirstName != user.FirstName)
        {
            var setFirstNameResult = await _userManager.UpdateAsync(user);
            if (!setFirstNameResult.Succeeded)
                throw new ApplicationException($"Unexpected error occurred setting first name for user with ID '{user.Id}'.");
        }

        if (userViewModel.LastName != user.LastName)
        {
            var setLastNameResult = await _userManager.UpdateAsync(user);
            if (!setLastNameResult.Succeeded)
                throw new ApplicationException($"Unexpected error occurred setting last name for user with ID '{user.Id}'.");
        }

        if (userViewModel.State != user.State)
        {
            var setStateResult = await _userManager.UpdateAsync(user);
            if (!setStateResult.Succeeded)
                throw new ApplicationException($"Unexpected error occurred setting city for user with ID '{user.Id}'.");
        }

        if (userViewModel.City != user.City)
        {
            var setCityResult = await _userManager.UpdateAsync(user);
            if (!setCityResult.Succeeded)
                throw new ApplicationException($"Unexpected error occurred setting city for user with ID '{user.Id}'.");
        }

        if (userViewModel.StreetAddress != user.StreetAddress)
        {
            var setStreetAddressResult = await _userManager.UpdateAsync(user);
            if (!setStreetAddressResult.Succeeded)
                throw new ApplicationException($"Unexpected error occurred setting street address for user with ID '{user.Id}'.");
        }

        if (userViewModel.PostalCode != user.PostalCode)
        {
            var setPostalCodeResult = await _userManager.UpdateAsync(user);
            if (!setPostalCodeResult.Succeeded)
                throw new ApplicationException($"Unexpected error occurred setting postal code for user with ID '{user.Id}'.");
        }

        StatusMessage = "Your profile has been updated";
        return RedirectToAction(nameof(MyAccount));
    }

    [HttpGet]
    public async Task<IActionResult> SetPassword()
    {
        var user = await _userManager.GetUserAsync(User)
            ?? throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        var hasPassword = await _userManager.HasPasswordAsync(user);

        if (hasPassword) return RedirectToAction(nameof(ChangePassword));

        var viewModel = new SetPasswordViewModel { StatusMessage = StatusMessage };

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
        StatusMessage = "Your password has been set.";

        return RedirectToAction(nameof(SetPassword));
    }

    [HttpGet]
    public async Task<IActionResult> ChangePassword()
    {
        var user = await _userManager.GetUserAsync(User)
            ?? throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        var hasPassword = await _userManager.HasPasswordAsync(user);

        if (!hasPassword) return RedirectToAction(nameof(SetPassword));

        var viewModel = new ChangePasswordViewModel { StatusMessage = StatusMessage };
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
        StatusMessage = "Your password has been changed.";

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
