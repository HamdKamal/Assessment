using Core.ViewModel;
using Databases.Models.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Assessment.Controllers
{
    public class LoginController : BaseController
    {
        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;

        public LoginController(UserManager<Users> userManager , SignInManager<Users> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Login");

        }
        // GET: LoginController
        public  IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginVM vm)
        {
            var result = new LoginVM();
            // check if the model is valid
            if (vm.Username == null)
            {
                result.Message = "Invalid Username or Password";
                return View(result);
            }
            if (vm.Password == null)
            {
                result.Message = "Invalid Username or Password";
                return View(result);
            }
            var user = await _userManager.FindByNameAsync(vm.Username);
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, vm.Password))
                {
                    if (!await _signInManager.CanSignInAsync(user))
                    {
                        result.Message = "<li>Invalid Username or Password</li>";
                        return View(result);
                    }
                    var signInResult = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, true,lockoutOnFailure: false);
                    if (signInResult.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                result.Message = "You don't have an account, Please Register !";
                return View(result);
            }
            result.Message = "Invalid Username or Password";
            return View(result);
        }
    }
}
