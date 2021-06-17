using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M2Link.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Nom")]
        public string Nom { get; set; }

        [DisplayName("Prénom")]
        public string Prenom { get; set; }

        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Pseudo")]
        [Required]
        public string Pseudo { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Mot de passe")]
        [Required]
        public string Mdp { get; set; }
    }
}