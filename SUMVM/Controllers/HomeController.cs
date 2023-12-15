using Microsoft.AspNetCore.Mvc;
using SUMVM.Repositories.Abstract;
namespace SUMVM.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;
        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public IActionResult Index(string term = "", int currentPage = 1)
        {
			// Retrieve the email and name from TempData
			var userEmail = Request.Cookies["UserEmail"];
			var userName = Request.Cookies["UserName"];
			var sessionKey = Request.Cookies["UserEmail"];

			// Pass the email and name to the Index view
			ViewBag.UserEmail = userEmail;
			ViewBag.UserName = userName;
			ViewBag.SessionKey = sessionKey;

			var movies = _movieService.List(term, true, currentPage);
            return View(movies);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult MovieDetail(int movieId)
        {
            var movie = _movieService.GetById(movieId);
            return View(movie);
        }

    }

}