using M2Link.Context;
using M2Link.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace M2Link.Repositories
{
    public class UserRepository
    {
        private static M2LinkContext Context;

        public UserRepository(M2LinkContext m2Link)
        {
            Context = m2Link;
        }

        public void Add(User user)
        {
            Context.Users.Add(user);
            Context.SaveChanges();
        }

        public List<User> GetAll()
        {
            return Context.Users.ToList();
        }

        public bool ExistPseudo(string pseudo)
        {
            return Context.Users.ToList().Exists(user => user.Pseudo == pseudo);
        }

        public User GetUser(string pseudo)
        {
            if (!ExistPseudo(pseudo))
            {
                return null;
            }
            return Context.Users.ToList().Find(user => user.Pseudo == pseudo);
        }

        public void Update(User user)
        {
            var index = Context.Users.ToList().FindIndex(u => u.Id.Equals(user.Id));
            if (index != -1)
            {
                DbSet<User> Users = Context.Users;
                Context.Users.ToList()[index] = user;
                Context.SaveChanges();
            }
        }

    }
}