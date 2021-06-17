using M2Link.Context;
using M2Link.Entities;
using M2Link.Models;
using M2Link.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace M2Link.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        // GET: UsersList
        public ActionResult Index()
        {
            using (var context = new M2LinkContext())
            {
                UserRepository repository = new UserRepository(context);
                ViewBag.UsersList = repository.GetAll().OrderBy(user => user.Pseudo).ToList();
                return View();
            }

        }

        public ActionResult Profil(Guid id)
        {
            using (var context = new M2LinkContext())
            {
                UserRepository repository = new UserRepository(context);
                FollowerRepository followRepository = new FollowerRepository(context);
                List<User> list = repository.GetAll();

                if (list.Count > 0 && list.Find(u => u.Id == id) != null)
                {
                    User user = list.Find(u => u.Id == id);
                    string Pseudo = (string)TempData["Pseudo"];
                    TempData["Pseudo"] = Pseudo;
                    User myUser = repository.GetUser(Pseudo);
                    ViewBag.Follow = followRepository.GetAllFollowForUser(user.Id).Exists(userCurrent => userCurrent.Id == myUser.Id);
                    ViewBag.IsMe = id.Equals(myUser.Id);
                    return View(user);
                }
                else
                {
                    return RedirectToAction("Index", "Users");
                }
            }
        }

        public ActionResult Follow(Guid id)
        {
            using (var context = new M2LinkContext())
            {
                UserRepository repository = new UserRepository(context);
                FollowerRepository followRepository = new FollowerRepository(context);
                string Pseudo = (string)TempData["Pseudo"];
                TempData["Pseudo"] = Pseudo;
                User myUser = repository.GetUser(Pseudo);
                followRepository.AddFollower(myUser.Id, id);
                return RedirectToAction("Profil", "Users", new { id });
            }
        }


        public ActionResult Unfollow(Guid id)
        {
            using (var context = new M2LinkContext())
            {
                UserRepository repository = new UserRepository(context);
                FollowerRepository followRepository = new FollowerRepository(context);
                string Pseudo = (string)TempData["Pseudo"];
                TempData["Pseudo"] = Pseudo;
                User myUser = repository.GetUser(Pseudo);
                followRepository.DeleteFollower(myUser.Id, id);

                return RedirectToAction("Profil", "Users", new { id });
            }
        }

    }
}