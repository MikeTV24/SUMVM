using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SUMVM.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task Login()
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



        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return View("Index");
        }
    }
}
