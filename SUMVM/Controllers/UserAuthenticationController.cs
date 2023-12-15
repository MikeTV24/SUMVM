using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SUMVM.Models.DTO;
using SUMVM.Repositories.Abstract;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Google;

namespace SUMVM.Controllers
{
    public class UserAuthenticationController : Controller
    {

        private IUserAuthenticationService authService;
        public UserAuthenticationController(IUserAuthenticationService authService)
        {
            this.authService = authService;
        }
        /* We will create a user with admin rights, after that we are going
          to comment this method because we need only
          one user in this application 
          If you need other users ,you can implement this registration method with view
          I have create a complete tutorial for this, you can check the link in description box
         */

          public async Task<IActionResult> Register()
          {
           var model = new RegistrationModel
           {
             Email = "admin@gmail.com",
             Username = "admin",
             Name = "John Michael",
            Password = "admin",
            PasswordConfirm = "admin",
            Role = "Admin"
          };

            // if you want to register with user , Change Role="User"
            var result = await authService.RegisterAsync(model);
           return Ok(result.Message);
         }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await authService.LoginAsync(model);
            if (result.StatusCode == 1)
                return RedirectToAction("Index", "Home");
            else
            {
                TempData["msg"] = "Could not logged in..";
                return RedirectToAction(nameof(Login));
            }
        }

        public async Task Login2()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleResponse")
                });
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Retrieve the user's email from claims
            var userEmail = result.Principal.FindFirstValue(ClaimTypes.Email);
            var userName = result.Principal.FindFirstValue(ClaimTypes.Name);

            HttpContext.Session.SetString("MySessionKey", userEmail);


            Response.Cookies.Append("UserEmail", userEmail);
            Response.Cookies.Append("UserName", userName);
            Response.Cookies.Append("SessionKey", userEmail);


            // Store the email in TempData to pass it to the Index action


            return RedirectToAction("Index", "Home", new { area = "" });
        }



        public async Task<IActionResult> Logout2()
        {
            await HttpContext.SignOutAsync();
            return View("Index");
        }


        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

    }
}