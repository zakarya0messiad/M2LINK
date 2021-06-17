using M2Link.Context;
using M2Link.Entities;
using M2Link.Repositories;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web.Mvc;
using System.Web.Security;

namespace M2Link.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Form()
        {
            ViewBag.Error = "";
            return View();
        }

        [HttpPost]
        public ActionResult Form(User user)
        {

            using (var context = new M2LinkContext())
            {
                UserRepository repository = new UserRepository(context);
                List<User> list = repository.GetAll();
                byte[] hashMdp = System.Text.Encoding.ASCII.GetBytes(user.Mdp);
                SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
                string hash = BitConverter.ToString(sha.ComputeHash(hashMdp)).Replace("-", "");
                if (list.Count > 0 && list.Find(u => u.Pseudo == user.Pseudo && u.Mdp == hash) != null)
                {

                    user = list.Find(u => u.Pseudo == user.Pseudo && u.Mdp == hash);
                    //FormsAuthentication.SetAuthCookie(user.Pseudo, true);
                    FormsAuthentication.RedirectFromLoginPage(user.Pseudo, true);
                    TempData["Pseudo"] = user.Pseudo;
                    ViewBag.Error = "";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Login ou mot de passe incorrect";
                    return View(new User());
                }
            }

        }
    }
}