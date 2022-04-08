using AG.Identity.Entities;
using AG.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AG.Identity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _rolemanager;
        public RoleController(RoleManager<AppRole> rolemanager)
        {
            _rolemanager = rolemanager;
        }
        public IActionResult Index()
        {
            var roles = _rolemanager.Roles.ToList();

            return View(roles);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleAdminModel model)
        {
            if (ModelState.IsValid)
            {
                AppRole appRole = new AppRole()
                {
                    Name=model.RoleName,
                    CreatedTime=DateTime.Now
                };
                var result = await _rolemanager.CreateAsync(appRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach(var role in result.Errors)
                    {
                        ModelState.AddModelError("", role.Description);
                    }
                }
            }
            return View();
        }
    }
}
