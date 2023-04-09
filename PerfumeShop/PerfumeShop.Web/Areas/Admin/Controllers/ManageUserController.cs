namespace PerfumeShop.Web.Areas.Admin.Controllers;


[Area("Admin")]
[Authorize(Roles = "Admin")]
[Route("[area]/[controller]/[action]")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ManageUserController : Controller
{
    private readonly UserManager<AppUser> _userManager;


    public ManageUserController(
        UserManager<AppUser> userManager,
    RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
    }


    [HttpGet]
    public async Task<IActionResult> Index(string role)
    {
        TempData["role"] = role;
        TempData.Keep("role");
        return View();
    }

    #region API CALLS
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userManager.GetUsersInRoleAsync(TempData["role"].ToString());
        return Json(new { data = users });
    }
    #endregion
}
