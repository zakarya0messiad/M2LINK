using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace M2Link.Entities
{
    public class Followers
    {
        [Key, Column(Order = 0)]
        public Guid IdFollow { get; set; }

        [Key, Column(Order = 1)]
        public Guid IdFollower { get; set; }
    }
}