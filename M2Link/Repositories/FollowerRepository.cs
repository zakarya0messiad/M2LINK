using M2Link.Context;
using M2Link.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2Link.Repositories
{
    public class FollowerRepository
    {
        private static M2LinkContext Context;

        public FollowerRepository(M2LinkContext m2Link)
        {
            Context = m2Link;
        }

        //retourne la liste de tous les suiveurs de userId
        public List<User> GetAllFollowerForUser(Guid userId)
        {
            List<User> users = new List<User>();
            List<User> allUsers = Context.Users.ToList();
            foreach (Followers f in Context.Followers.ToList())
            {
                if (f.IdFollow == userId)
                {
                    users.Add(allUsers.Find(user => user.Id == f.IdFollower));
                }
            }
            return users;
        }

        //retourne la liste de tout ceux que userId suit
        public List<User> GetAllFollowForUser(Guid userId)
        {
            List<User> users = new List<User>();
            List<User> allUsers = Context.Users.ToList();
            foreach (Followers f in Context.Followers.ToList())
            {
                if (f.IdFollower == userId)
                {
                    users.Add(allUsers.Find(user => user.Id == f.IdFollow));
                }
            }
            return users;
        }

        public void AddFollower(Guid followId, Guid followerId)
        {
            Followers f = new Followers
            {
                IdFollow = followId,
                IdFollower = followerId
            };
            Context.Followers.Add(f);
            Context.SaveChanges();
        }

        public void DeleteFollower(Guid followId, Guid followerId)
        {
            Followers f = Context.Followers.ToList().Find(row => row.IdFollow == followId && row.IdFollower == followerId);

            Context.Followers.Remove(f);
            Context.SaveChanges();
        }
    }
}