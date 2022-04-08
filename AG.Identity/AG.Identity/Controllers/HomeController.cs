using AG.Identity.Entities;
using AG.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AG.Identity.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserModel user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
            {
                UserName = user.UserName,
                Email = user.Email,
                Gender = user.Gender,
            };
     
                var identiyresult = await _userManager.CreateAsync(appUser, user.Password);
                if (identiyresult.Succeeded) {
                    
                        await _roleManager.CreateAsync(new AppRole()
                        {
                            Name = "Member",
                            CreatedTime = DateTime.Now,
                        });
                    
                    await _userManager.AddToRoleAsync(appUser, "Member");
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in identiyresult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(user);
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);             
                var message = string.Empty;
                if (signInResult.Succeeded)
                {               
                   var role = await _userManager.GetRolesAsync(user);
                    if (role.Contains("Admin"))
                    {
                        return RedirectToAction("AdminPanel");
                    }
                    else
                    {
                        return RedirectToAction("Panel");
                    }
                }
                
                else if (signInResult.IsLockedOut)
                {
                    var lockoutEnd = await _userManager.GetLockoutEndDateAsync(user);
                    message = $"Hesabınız {(lockoutEnd.Value.UtcDateTime - DateTime.UtcNow).Minutes} Dakika Askıya Alınmıştır.";
                }
               
                else
                {
                   if(user != null)
                    {
                        var KilitSayisi = await _userManager.GetAccessFailedCountAsync(user);
                        message = $"{(_userManager.Options.Lockout.MaxFailedAccessAttempts - KilitSayisi)} kez hakkınız kaldı";
                    }
                    else
                    {
                        message = "Kullanıcı Adı veya Şifre Hatalı";
                    }
                }
                
                ModelState.AddModelError("", message);
               
            }

            return View(model);
          
        }
         
    
        [Authorize]
        public IActionResult GetUserInfo()
        {
            return View();
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }

        [Authorize(Roles ="Member")]
        public IActionResult Panel()
        {
            return View();
        }

        public async Task<IActionResult> SignOut()
        {
           await _signInManager.SignOutAsync();
           return RedirectToAction("Index");
        }

    
        public IActionResult AccessDenied()
        {
            return View();
        }
    }   
}
