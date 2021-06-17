using M2Link.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace M2Link.WebServices
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IWSUser" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IWSUser
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        User Validate(string login, string mdp);

        [OperationContract]
        bool VerifyExistPseudo(string Pseudo);

        [OperationContract]
        void AddUser(User User);

        [OperationContract]
        List<User> GetListUser();

        [OperationContract]
        void ModifyUser(User user);

        [OperationContract]
        User GetUser(Guid myId);

        [OperationContract]
        int GetNbFollowers(Guid myId);
    }
}
