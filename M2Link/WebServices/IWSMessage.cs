using M2Link.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace M2Link.WebServices
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IWSMessage" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IWSMessage
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        string HelloWorld();

        [OperationContract]
        List<Message> GetListMyMessages(Guid id);

        [OperationContract]
        void AddMessage(Guid ownerId, string message, string pseudo);

        [OperationContract]
        List<Message> GetListMessagesFollow(Guid id);

        [OperationContract]
        int GetNbFollow(Guid id);

        [OperationContract]
        bool IsFollow(Guid myId, Guid hisId);

        [OperationContract]
        void Follow(Guid myId, Guid hisId);

        [OperationContract]
        void UnFollow(Guid myId, Guid hisId);
    }
}
