 using AG.Identity.Entities;
using AG.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AG.Identity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var memberUser = await _userManager.GetUsersInRoleAsync("Member");
            return View(memberUser);
        }
    
        public IActionResult Create()
        {
            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateUserAdminModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    UserName = model.UserName,
                    Gender = model.Gender,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(appUser,model.UserName+"123");
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach(var user in result.Errors)
                    {
                        ModelState.AddModelError("",user.Description);
                    }
                }
            };
            
            return View();
        }
    }
}
