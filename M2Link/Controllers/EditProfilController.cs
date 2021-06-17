using M2Link.Context;
using M2Link.Entities;
using M2Link.Models;
using M2Link.Repositories;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web.Mvc;

namespace M2Link.Controllers
{
    [Authorize]
    public class EditProfilController : Controller
    {

        public ActionResult Form()
        {
            string Pseudo = (string)TempData["Pseudo"];
            TempData["Pseudo"] = Pseudo;
            using (var context = new M2LinkContext())
            {
                UserRepository userRepository = new UserRepository(context);
                User user = userRepository.GetUser(Pseudo);
                RegisterModel model = new RegisterModel()
                {
                    Nom = user.Nom,
                    Prenom = user.Prenom,
                    Email = user.Email,
                    Pseudo = user.Pseudo,
                    Mdp = "",
                    Verif_mdp = ""
                };
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Form(RegisterModel register)
        {
            string Pseudo = (string)TempData["Pseudo"];
            TempData["Pseudo"] = Pseudo;
            register.Pseudo = Pseudo;
            if (ModelState.IsValid)
            {
                using (var context = new M2LinkContext())
                {
                    UserRepository repository = new UserRepository(context);

                    User userModify = repository.GetUser(Pseudo);

                    byte[] hashMdp = System.Text.Encoding.ASCII.GetBytes(register.Mdp);
                    SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
                    string hash = BitConverter.ToString(sha.ComputeHash(hashMdp)).Replace("-", "");

                    userModify.Nom = register.Nom;
                    userModify.Prenom = register.Prenom;
                    userModify.Email = register.Email;
                    userModify.Mdp = hash;
                    repository.Update(userModify);
                    context.SaveChanges();
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(register);
            }

        }

        public ActionResult Profil()
        {
            string Pseudo = (string)TempData["Pseudo"];
            TempData["Pseudo"] = Pseudo;
            using (var context = new M2LinkContext())
            {
                UserRepository repository = new UserRepository(context);
                User user = repository.GetUser(Pseudo);
                return View(user);
            }
        }
    }
}