using Microsoft.AspNetCore.Mvc;
using MvcWhatsUp.Models;

namespace MvcWhatsUp.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            List<User> users =
                [
                new User(1, "Peter Sauber", "06-12345678", "pete.sauber@gmail.com"),
                new User(2, "Berkay Yalcin", "05-87263415", "berk.yalcin@gmail.com"),
                new User(3, "Gabriele Amorosi", "02-87654321", "gabriele.amorosi@gmail.com")
                ];
            //return the view
            return View(users);
        }
    }
}
