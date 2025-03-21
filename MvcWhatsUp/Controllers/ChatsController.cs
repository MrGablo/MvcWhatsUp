using Microsoft.AspNetCore.Mvc;
using MvcWhatsUp.Models;
using MvcWhatsUp.Repositories;

namespace MvcWhatsUp.Controllers
{
    public class ChatsController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IChatsRepository _chatsRepository;

        public ChatsController(IUsersRepository usersRepository, IChatsRepository chatsRepository)
        {
            _usersRepository = usersRepository;
            _chatsRepository = chatsRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddMessage(int? id)
        {
            // receiver user id (parameter) must be available
            if (id == null)
                return RedirectToAction("Index", "Users");

            // user needs to be logged in
            // (for now, id of logged in user is stored in a cookie)
            string? loggedInUserId = Request.Cookies["UserId"];
            if (loggedInUserId == null)
                return RedirectToAction("Index", "Users");

            // get the receiving User so we can show the name in the View
            User? receiverUser = _usersRepository.GetbyId((int)id);
            ViewData["ReceiverUser"] = receiverUser;

            Message message = new Message();
            message.SenderUserId = int.Parse(loggedInUserId);
            message.ReceiverUserId = (int)id;
            return View(message);
        }

        [HttpPost]
        public IActionResult AddMessage(Message message)
        {
            try
            {
                message.SendAt = DateTime.Now;
                _chatsRepository.AddMessage(message);

                //go to chat with the other user
                return RedirectToAction("DisplayChat", new { id = message.ReceiverUserId });
            }
            catch (Exception ex)
            {
                //something is wrong
                return View(message);
            }
        }

        [HttpGet]
        public IActionResult DisplayChat(int? id)
        {
            // receiver user id (parameter) must be available
            if (id == null)
                return RedirectToAction("Index", "Users");

            // user needs to be logged in
            // (for now, id of logged in user is stored in a cookie)
            string? loggedInUserId = Request.Cookies["UserId"];
            if (loggedInUserId == null)
                return RedirectToAction("Index", "Users");

            // Get User objects via users repository
            User? sendingUser = _usersRepository.GetbyId(int.Parse(loggedInUserId));
            User? receivingUser = _usersRepository.GetbyId((int)id);
            if ((sendingUser == null) || (receivingUser == null))
                return RedirectToAction("Index", "Users");

            // pass users to View via ViewData
            ViewData["sendingUser"] = sendingUser;
            ViewData["receivingUser"] = receivingUser;

            // get all messages between 2 users
            List<Message> chatMessages = _chatsRepository.GetMessages(
                sendingUser.UserId, receivingUser.UserId);

            // pass data to view
            return View(chatMessages);
        }

    }
}
