namespace PerfumeShop.Web.Areas.Admin.Controllers;


[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ManageUserController : Controller
{
    private readonly IMapper _mapper;
    private readonly IUserStore<AppUser> _userStore;
    private readonly ILogger<ManageUserController> _logger;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;   
    private readonly SignInManager<AppUser> _signInManager;

    public ManageUserController(
        IMapper mapper,
        IUserStore<AppUser> userStore,
        ILogger<ManageUserController> logger,
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
		SignInManager<AppUser> signInManager)
    {
        _mapper = mapper;
        _userStore = userStore;
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }


    [HttpGet]
    public IActionResult Index() => View();


    [HttpGet]
    public IActionResult Create()
    {
        var user = new CreateUserViewModel
        {
            RoleList = GetRoleNames().ToSelectListItems()
        };
        return View(user);
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateUserViewModel userView)
    {
        if (ModelState.IsValid)
        {
            var user = _mapper.Map<AppUser>(userView);
            await _userStore.SetUserNameAsync(user, userView.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, userView.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation($"User {userView.Email} created a new account with password.");
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

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user is null) return NotFound();

        var rolesOfUser = await _userManager.GetRolesAsync(user);
        var userView = _mapper.Map<EditUserViewModel>(user);
        userView.Role = rolesOfUser.FirstOrDefault();
        userView.RoleList = GetRoleNames().ToSelectListItems();

        var currentUser = await _userManager.GetUserAsync(HttpContext.User);
        ViewBag.CurrentUserId = currentUser.Id;

		return View(userView);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditUserViewModel userView)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(userView.Id);

            if (user is not null)
            {
                user.FirstName = userView.FirstName;
                user.LastName = userView.LastName;
                user.State = userView.State;
                user.City = userView.City;
                user.StreetAddress = userView.StreetAddress;
                user.PhoneNumber = userView.PhoneNumber;
                user.PostalCode = userView.PostalCode;
                var result = await _userManager.UpdateAsync(user);
                
                if (result.Succeeded)
                {
					_logger.LogInformation($"User {userView.Email} was updated.");
					var roles = await _userManager.GetRolesAsync(user);
                    var role = roles.FirstOrDefault();

                    if (!role.Equals(userView.Role, StringComparison.CurrentCultureIgnoreCase))
                    {						
						await _userManager.RemoveFromRoleAsync(user, role);
                        await _userManager.AddToRoleAsync(user, userView.Role);
						_logger.LogInformation($"Set role - {userView.Role} for user id - {userView.Id}.");
					}

					TempData["success"] = $"User {userView.Email} was updated.";

					return RedirectToAction(nameof(Index), new { role = userView.Role });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
        }
        userView.RoleList = GetRoleNames().ToSelectListItems();

		return View(userView);
    }


    #region API CALLS
    [HttpGet]
    public async Task<JsonResult> GetAll()
    {
        var users = await _userManager.Users
            .Include(u => u.UserRoles)
            .ThenInclude(r => r.Role)
            .ToListAsync();

        var usersWithRole = _mapper.Map<List<UserWithRoleViewModel>>(users);

        return Json(new { data = usersWithRole });
    }


    [HttpDelete]
    public async Task<JsonResult> Delete(string id)
    {
        var userView = await _userManager.FindByIdAsync(id);

        if (userView is null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }

        var currentUser = await _userManager.GetUserAsync(HttpContext.User);
        if (currentUser.Id.Equals(id)) 
        {
            return Json(new { success = false, message = "This is your account!" });
        }

        await _userManager.DeleteAsync(userView);

        return Json(new { success = true, message = $"{userView.UserName} was deleted successfully" });
    }
    #endregion

    private IEnumerable<string> GetRoleNames() 
        => _roleManager.Roles.Select(Role => Role.Name).ToList();      
}