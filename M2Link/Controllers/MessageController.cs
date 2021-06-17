using M2Link.Context;
using M2Link.Entities;
using M2Link.Repositories;
using System;
using System.Linq;
using System.Web.Mvc;

namespace M2Link.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new M2LinkContext())
            {
                MessageRepository repository = new MessageRepository(context);
                UserRepository userRepository = new UserRepository(context);
                string Pseudo = (string)TempData["Pseudo"];
                TempData["Pseudo"] = Pseudo;
                User user = userRepository.GetUser(Pseudo);
                return View(repository.GetAllMessageForUser(user.Id).OrderByDescending(msg => msg.PostDate).ToList());
            }
        }

        public ActionResult Create()
        {
            return View(new Message());
        }

        [HttpPost]
        public ActionResult Create(Message m)
        {
            using (var context = new M2LinkContext())
            {
                Guid messageId = Guid.NewGuid();
                UserRepository userRepository = new UserRepository(context);
                string Pseudo = (string)TempData["Pseudo"];
                TempData["Pseudo"] = Pseudo;
                User user = userRepository.GetUser(Pseudo);

                Message message = new Message
                {
                    Id = messageId,
                    PostDate = DateTime.UtcNow,
                    Content = m.Content,
                    OwnerId = user.Id,
                    OwnerPseudo = Pseudo
                };

                MessageRepository repository = new MessageRepository(context);
                repository.AddMessage(message);
            }

            return RedirectToAction("Index", "Message");
        }

        public ActionResult Return()
        {
            return RedirectToAction("Index", "Home");
        }
    }

}