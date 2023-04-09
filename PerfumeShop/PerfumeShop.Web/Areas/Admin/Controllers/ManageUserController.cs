namespace PerfumeShop.Web.Areas.Admin.Controllers;


[Area("Admin")]
[Authorize(Roles = "Admin")]
[Route("[area]/[controller]/[action]")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ManageUserController : Controller
{
    private readonly IMapper _mapper;
    private readonly IUserStore<AppUser> _userStore;
    private readonly ILogger<ManageUserController> _logger;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;


    public ManageUserController(
        IMapper mapper,
        IUserStore<AppUser> userStore,
        ILogger<ManageUserController> logger,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _mapper = mapper;
        _userStore = userStore;
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
    }


    [HttpGet]
    public async Task<IActionResult> Index(string role)
    {
        TempData["role"] = role;
        TempData.Keep("role");
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        var user = new RegisterUserViewModel
        {
            RoleList = GetRoleNames().ToSelectListItems()
    };
        return View(user);
    }


    [HttpPost]
    public async Task<IActionResult> Create(RegisterUserViewModel userView)
    {
        if (ModelState.IsValid)
        {
            var user = _mapper.Map<AppUser>(userView);
            await _userStore.SetUserNameAsync(user, userView.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, userView.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                await _userManager.AddToRoleAsync(user, userView.Role);
                TempData["success"] = $"{userView.Role} was created successfully!";
                return RedirectToAction(nameof(Index), new { role = userView.Role });
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            userView.RoleList = GetRoleNames().ToSelectListItems();
        }
        return View(userView);
    }


    #region API CALLS
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userManager.GetUsersInRoleAsync(TempData["role"].ToString());
        return Json(new { data = users });
    }
    #endregion



    private IEnumerable<string> GetRoleNames() => _roleManager.Roles.Select(Role => Role.Name).ToList();      
}
