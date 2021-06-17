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
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "WSLogin" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez WSLogin.svc ou WSLogin.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class WSLogin : IWSLogin
    {
        public void DoWork()
        {
        }

        public String HelloWorld()
        {
            return "Hello World";
        }
        /*
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
        }*/
    }
}
