using M2Link.Context;
using M2Link.Entities;
using M2Link.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace M2Link.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            string Pseudo = (string)TempData["Pseudo"];
            TempData["Pseudo"] = Pseudo;
            if (Pseudo == null) {
                return RedirectToAction("Form", "Login");
            }
            using (var context = new M2LinkContext())
            {
                UserRepository userRepository = new UserRepository(context);
                FollowerRepository followRepository = new FollowerRepository(context);
                MessageRepository messageRepository = new MessageRepository(context);
                User user = userRepository.GetUser(Pseudo);
                List<User> follow = followRepository.GetAllFollowerForUser(user.Id);
                ViewBag.nbFollow = follow.Count();
                ViewBag.nbFollower = followRepository.GetAllFollowForUser(user.Id).Count();
                List<Message> messagesFollow = new List<Message>();
                foreach (User userFollow in follow)
                {
                    List<Message> messages = messageRepository.GetAllMessageForUser(userFollow.Id);
                    foreach (Message msg in messages)
                    {
                        messagesFollow.Add(msg);
                    }

                }
                ViewBag.messagesFollow = messagesFollow.OrderByDescending(msg => msg.PostDate).ToList();
                return View(user);
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            TempData["Pseudo"] = null;
            return RedirectToAction("Form", "Login");
        }
    }
}