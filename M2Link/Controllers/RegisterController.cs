using M2Link.Context;
using M2Link.Entities;
using M2Link.Models;
using M2Link.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace M2Link.Controllers
{
    public class RegisterController : Controller
    {


        // GET: Register
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Form(RegisterModel register)
        {
            using (var context = new M2LinkContext())
            {
                UserRepository repository = new UserRepository(context);
                if (repository.ExistPseudo(register.Pseudo))
                {
                    ViewBag.Error = "Pseudo est déjà utilisé !";
                    return View(register);
                }
                if (ModelState.IsValid)
                {

                    byte[] hashMdp = System.Text.Encoding.ASCII.GetBytes(register.Mdp);
                    SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
                    string hash = BitConverter.ToString(sha.ComputeHash(hashMdp)).Replace("-", "");

                    User user = new User
                    {
                        Id = Guid.NewGuid(),
                        Prenom = register.Prenom,
                        Nom = register.Nom,
                        Email = register.Email,
                        Pseudo = register.Pseudo,
                        Mdp = hash
                    };
                    repository.Add(user);
                    return RedirectToAction("Form", "Login");
                }
                else
                {
                    ViewBag.Error = "Erreur ^-";
                    return View(register);
                }
            }



        }
    }
}