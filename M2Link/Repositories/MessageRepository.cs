using M2Link.Context;
using M2Link.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace M2Link.Repositories
{
    public class MessageRepository
    {
        private static M2LinkContext Context;

        public MessageRepository(M2LinkContext m2Link)
        {
            Context = m2Link;
        }

        public List<Message> GetAllMessageForUser(Guid userId)
        {
            List<Message> messages = new List<Message>();
            Context.Messages.ToList();
            foreach (Message msg in Context.Messages.ToList())
            {
                if (msg.OwnerId == userId)
                {
                    messages.Add(msg);
                }
            }
            return messages;
        }

        public List<Message> GetAllMessage()
        {
            return Context.Messages.ToList();
        }

        public void AddMessage(Message message)
        {
            Context.Messages.Add(message);
            Context.SaveChanges();
        }
    }
}