using Microsoft.AspNetCore.Mvc;
using MvcWhatsUp.Models;
using MvcWhatsUp.Repositories;

namespace MvcWhatsUp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        public UsersController(IUsersRepository usersRepository) 
        {
            _usersRepository = usersRepository;
        }
        public IActionResult Index()
        {
            //get logged in user through cookie
            string? userId = Request.Cookies["UserId"];

            ViewData["UserId"] = userId;

            //get all the users
            List<User> users = _usersRepository.GetAll();
            //return the view
            return View(users);
        }

        //Get: UsersController/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //POST: UsersController/Create
        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                //add user via repository
                _usersRepository.Add(user);
                //go back to user list
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                //somethings wrong, go back to view with user
                return View(user);
            }
        }
        //Get: Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            //get user
            User? user = _usersRepository.GetbyId((int)id);
            return View(user);
        }
        //Post: Edit
        [HttpPost]
        public IActionResult Edit(User user)
        {
            try
            {
                _usersRepository.Update(user);

                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                return View(user);
            }
        }
        //Get: Delete
        [HttpGet]   
        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            User? user = _usersRepository.GetbyId((int)id);
            return View(user);
        }
        //POST: DELETE
        [HttpPost]
        public IActionResult Delete(User user)
        {
            try
            {
                _usersRepository.Delete(user);

                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                return View(user);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            User? user = _usersRepository.GetByLoginCredentials(loginModel.UserName, loginModel.Password);
            if(user == null)
            {
                return View(loginModel);
            }
            else
            {
                //cookie to remeber user
                Response.Cookies.Append("UserId", user.UserId.ToString());

                //redirect to list of users
                return RedirectToAction("index", "Users");
            }
        }
    }
}
