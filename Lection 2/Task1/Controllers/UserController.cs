using Microsoft.AspNetCore.Mvc;
using Task1.Models;

namespace Task1.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult UserAccount() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserRegistration(UserModel user, UserManagerModel user_method)
        {
            if(!user_method.create_user(user.UserName, user.Password, user.IsAdmin))
            {
                ViewData["ErrorMessage"] = "Such a user already exists.";
                return View("SignUp");
            }

            return View("Login");
        }

        [HttpPost]
        public IActionResult UserVerification (UserModel user, UserManagerModel user_method)
        {
            //Add a check for user existence.
            if (user_method.user_data_verification(user.UserName, user.Password)) {

                if (user_method.admin_check(user.UserName))
                {
                    Console.WriteLine("Is Admin");
                    user_method.user_info(user.UserName);
                    return View("Admin");
                }
                else
                {
                    Console.WriteLine("Is not admin");
                    user_method.user_info(user.UserName);
                    return View("UserAccount", user);
                }
            }
            else
            {
                ViewData["ErrorMessage"] = "Invalid login or password.";
                return View("Login");
            }
        }
    }
}
