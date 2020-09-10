using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Planter_API_2.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public UserType UserType { get; set; }
        [ForeignKey("UserType")]
        public int FK_UserTypeID { get; set; }

        //Relationships
        public List<Plants> Plants { get; set; }

        public List<Comments> Comments { get; set; }
    }
}
