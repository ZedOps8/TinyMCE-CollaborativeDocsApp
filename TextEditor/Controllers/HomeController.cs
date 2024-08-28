using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TextEditor.Models;

namespace TextEditor.Controllers
{
    // Controller for handling requests related to the home page
    public class HomeController : Controller
    {
        // Logger for logging information, warnings, and errors
        private readonly ILogger<HomeController> _logger;

        // Constructor to initialize the logger
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: / - Returns the view for the home page (Index view)
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Privacy - Returns the view for the privacy policy page
        public IActionResult Privacy()
        {
            return View();
        }

        // GET: /Error - Returns the view for error handling
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Create an ErrorViewModel with the current request ID or trace identifier
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
