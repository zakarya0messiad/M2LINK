using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace M2Link.Entities
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Contenu")]
        [DataType(DataType.Text)]
        public string Content { get; set; }

        public Guid OwnerId { get; set; }

        public string OwnerPseudo { get; set; }

        [DataType(DataType.Date)]
        public DateTime PostDate { get; set; }
    }
}