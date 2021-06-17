using M2Link.Context;
using M2Link.Entities;
using M2Link.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace M2Link.WebServices
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "WSMessage" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez WSMessage.svc ou WSMessage.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class WSMessage : IWSMessage
    {
        public void DoWork()
        {
        }

        public string HelloWorld()
        {
            return "Hello World";
        }

        public List<Message> GetListMyMessages(Guid id)
        {
            using (var context = new M2LinkContext())
            {
                MessageRepository repository = new MessageRepository(context);
                return repository.GetAllMessageForUser(id).OrderByDescending(msg => msg.PostDate).ToList();
            }
        }

        public void AddMessage(Guid ownerId, string message, string pseudo)
        {
            using (var context = new M2LinkContext())
            {
                UserRepository userRepository = new UserRepository(context);
                Guid messageId = Guid.NewGuid();

                Message msg = new Message
                {
                    Id = messageId,
                    Content = message,
                    PostDate = DateTime.UtcNow,
                    OwnerId = ownerId,
                    OwnerPseudo = pseudo
                };

                MessageRepository repository = new MessageRepository(context);
                repository.AddMessage(msg);
            }
        }

        public List<Message> GetListMessagesFollow(Guid id)
        {
            using (var context = new M2LinkContext())
            {
                FollowerRepository followRepository = new FollowerRepository(context);
                MessageRepository messageRepository = new MessageRepository(context);
                List<User> follow = followRepository.GetAllFollowerForUser(id);
                List<Message> messagesFollow = new List<Message>();
                foreach (User userFollow in follow)
                {
                    List<Message> messages = messageRepository.GetAllMessageForUser(userFollow.Id);
                    foreach (Message msg in messages)
                    {
                        messagesFollow.Add(msg);
                    }
                }
                return messagesFollow.OrderByDescending(msg => msg.PostDate).ToList();
            }
        }

        public int GetNbFollow(Guid id)
        {
            using (var context = new M2LinkContext())
            {
                FollowerRepository followRepository = new FollowerRepository(context);
                return followRepository.GetAllFollowerForUser(id).Count();
            }
        }

        public bool IsFollow(Guid myId, Guid hisId)
        {
            using (var context = new M2LinkContext())
            {
                FollowerRepository followRepository = new FollowerRepository(context);
                return followRepository.GetAllFollowForUser(hisId).Exists(userCurrent => userCurrent.Id == myId);
            }
        }

        public void Follow(Guid myId, Guid hisId)
        {
            using (var context = new M2LinkContext())
            {
                FollowerRepository followRepository = new FollowerRepository(context);
                followRepository.AddFollower(myId, hisId);
            }
        }

        public void UnFollow(Guid myId, Guid hisId)
        {
            using (var context = new M2LinkContext())
            {
                FollowerRepository followRepository = new FollowerRepository(context);
                followRepository.DeleteFollower(myId, hisId);
            }
        }
    }
}

