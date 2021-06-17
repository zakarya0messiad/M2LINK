using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M2Link.Models
{
    public class RegisterModel
    {

        private String nom;
        private String prenom;
        private String email;
        private String pseudo;
        private String mdp;
        private String verif_mdp;

        [Required]
        [DataType(DataType.Text)]

        [DisplayName("Nom de famille")]
        public string Nom { get => nom; set => nom = value; }

        [Required]
        [DataType(DataType.Text)]

        [DisplayName("Prénom")]
        public string Prenom { get => prenom; set => prenom = value; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get => email; set => email = value; }

        [Required]
        [DataType(DataType.Text)]
        public string Pseudo { get => pseudo; set => pseudo = value; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+=/{};:<>|./?,-]).{8,}$", ErrorMessage = "Mot de passe invalide")]
        [StringLength(20, ErrorMessage = "Au moins 8 caractères", MinimumLength = 8)]
        [DisplayName("Mot de passe")]
        public string Mdp { get => mdp; set => mdp = value; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirmation du mot de passe")]
        [Compare("Mdp", ErrorMessage = "Les mots de passe ne sont pas identiques")]
        public string Verif_mdp { get => verif_mdp; set => verif_mdp = value; }
    }
}