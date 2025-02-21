using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcWhatsUp.Models;

namespace MvcWhatsUp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public string GetAllStudents()
        {
            return "Displaying all students: \nHarry, Berkay, Hamza, Dylan,\nHunter, Gabriele, Anastasia";
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Chats()
        {
            return View();
        }
        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult SendMessage()
        {
            return View();
        }
        [HttpPost]
        public string SendMessage(string message, string name)
        {
             return $"Message {message} has been sent by {name}";
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public string Login(string email, string password)
        {
            return $"Email: {email}, Password: {password}";
        }
    }
}
