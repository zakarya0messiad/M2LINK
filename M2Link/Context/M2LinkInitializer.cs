using M2Link.Entities;
using System;
using System.Data.Entity;
using System.Security.Cryptography;

namespace M2Link.Context
{
    public class M2LinkInitializer : DropCreateDatabaseIfModelChanges<M2LinkContext>
    {

        protected override void Seed(M2LinkContext Context)
        {
            base.Seed(Context);

            byte[] hashMdp1 = System.Text.Encoding.ASCII.GetBytes("Bbob21@@");
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            string hash1 = BitConverter.ToString(sha1.ComputeHash(hashMdp1)).Replace("-", "");
            User User1 = new User
            {
                Id = Guid.NewGuid(),
                Pseudo = "bob",
                Nom = "Marley",
                Prenom = "Bob",
                Email = "bob@reggaemail.jm",
                Mdp = hash1
            };
            Context.Users.Add(User1);

            byte[] hashMdp2 = System.Text.Encoding.ASCII.GetBytes("2Packs@@");
            SHA1CryptoServiceProvider sha2 = new SHA1CryptoServiceProvider();
            string hash2 = BitConverter.ToString(sha2.ComputeHash(hashMdp2)).Replace("-", "");
            User User2 = new User
            {
                Id = Guid.NewGuid(),
                Pseudo = "2pac",
                Nom = "Shakur",
                Prenom = "Tupac",
                Email = "2pac@rapmail.us",
                Mdp = hash2
            };
            Context.Users.Add(User2);

            Message msgbob1 = new Message
            {
                Id = Guid.NewGuid(),
                Content = "Ne vis pas pour que ta présence se remarque, mais pour que ton absence se ressente.",
                OwnerId = User1.Id,
                OwnerPseudo = User1.Pseudo,
                PostDate = DateTime.Today
            };
            Context.Messages.Add(msgbob1);

            Message msgbob2 = new Message
            {
                Id = Guid.NewGuid(),
                Content = "Tu ne sais pas à quel point tu es fort jusqu'au jour où être fort devient la seule option.",
                OwnerId = User1.Id,
                OwnerPseudo = User1.Pseudo,
                PostDate = DateTime.Today
            };
            Context.Messages.Add(msgbob2);

            Message msgbob3 = new Message
            {
                Id = Guid.NewGuid(),
                Content = "Mieux vaut  mourir pour la liberté, plutôt qu'être un prisonnier tous les jours de ta vie.",
                OwnerId = User1.Id,
                OwnerPseudo = User1.Pseudo,
                PostDate = DateTime.Today
            };
            Context.Messages.Add(msgbob3);

            Message msgbob4 = new Message
            {
                Id = Guid.NewGuid(),
                Content = "Le jour où tu arrêteras de courir, c'est le jour où tu gagneras la course.",
                OwnerId = User1.Id,
                OwnerPseudo = User1.Pseudo,
                PostDate = DateTime.Today
            };
            Context.Messages.Add(msgbob4);

            Message msg2pac1 = new Message
            {
                Id = Guid.NewGuid(),
                Content = "Au cours de votre vie, n’arrêtez jamais de rêver. Personne ne peut vous enlever vos rêves.",
                OwnerId = User2.Id,
                OwnerPseudo = User2.Pseudo,
                PostDate = DateTime.Today
            };
            Context.Messages.Add(msg2pac1);

            Message msg2pac2 = new Message
            {
                Id = Guid.NewGuid(),
                Content = "Ce monde est rempli de personnes fausses, mais avant de juger, assurez-vous de ne pas en faire partie. ",
                OwnerId = User2.Id,
                OwnerPseudo = User2.Pseudo,
                PostDate = DateTime.Today
            };
            Context.Messages.Add(msg2pac2);

            Message msg2pac3 = new Message
            {
                Id = Guid.NewGuid(),
                Content = "Ce monde est rempli de personnes fausses, mais avant de juger, assurez-vous de ne pas en faire partie. ",
                OwnerId = User2.Id,
                OwnerPseudo = User2.Pseudo,
                PostDate = DateTime.Today
            };
            Context.Messages.Add(msg2pac3);

            Followers flw1 = new Followers
            {
                IdFollow = User1.Id,
                IdFollower = User2.Id
            };
            Context.Followers.Add(flw1);
            Followers flw2 = new Followers
            {
                IdFollow = User2.Id,
                IdFollower = User1.Id
            };
            Context.Followers.Add(flw2);

            Context.SaveChanges();
        }
    }
}