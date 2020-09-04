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
        public string Username { get; set; }
        public string Password { get; set; }

        public UserType UserType { get; set; }
        [ForeignKey("UserType")]
        public int UserTypeID { get; set; }

        //Relationships
        public List<Plants> Plants { get; set; }
    }
}
