using M2Link.Context;
using M2Link.Entities;
using M2Link.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;

namespace M2Link.WebServices
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "WSUser" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez WSUser.svc ou WSUser.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class WSUser : IWSUser
    {
        public void DoWork()
        {
        }
        
        public User Validate(String login, String mdp)
        {
            using (var context = new M2LinkContext())
            {
                UserRepository repository = new UserRepository(context);
                User user = repository.GetUser(login);
                if (user == null)
                {
                    return null;
                }

                byte[] hashMdp = System.Text.Encoding.ASCII.GetBytes(mdp);
                SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
                string hash = BitConverter.ToString(sha.ComputeHash(hashMdp)).Replace("-", "");

                if (user.Mdp == hash)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        /*
         * Vérifier que le pseudo n'est pas déja présent dans la base de données
         * Retourne le true s'il existe sinon false
         */
        public bool VerifyExistPseudo(string Pseudo)
        {
            using (var context = new M2LinkContext())
            {
                UserRepository repository = new UserRepository(context);
                return repository.ExistPseudo(Pseudo);
            }
        }

        /*
         * Ajoute l'utilisateur User dans la base de données
         * Retourne le true s'il existe sinon false
         */
        public void AddUser(User User)
        {
            using (var context = new M2LinkContext())
            {
                UserRepository repository = new UserRepository(context);
                repository.Add(User);
            }
        }

        public List<User> GetListUser()
        {
            using (var context = new M2LinkContext())
            {
                UserRepository repository = new UserRepository(context);
                return repository.GetAll().OrderBy(user => user.Pseudo).ToList();
            }
        }

        public void ModifyUser(User user)
        {
            using (var context = new M2LinkContext())
            {
                UserRepository repository = new UserRepository(context);
                repository.Update(user);
                context.SaveChanges();
            }
        }

        public User GetUser(Guid myId)
        {
            using (var context = new M2LinkContext())
            {
                UserRepository repository = new UserRepository(context);
                return repository.GetAll().Find(user => user.Id == myId);
            }
        }

        public int GetNbFollowers(Guid myId)
        {
            using (var context = new M2LinkContext())
            {
                FollowerRepository followRepository = new FollowerRepository(context);
                return followRepository.GetAllFollowForUser(myId).Count();
            }
        }
    }
}
