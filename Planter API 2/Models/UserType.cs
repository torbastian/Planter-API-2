using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Planter_API_2.Models
{
    public class UserType
    {
        [Key]
        public int UserTypeID { get; set; }
        [Required]
        public string UType { get; set; }

        //Relationships
        public List<Users> Users { get; set; }
    }

    [NotMapped]
    public class UserTypeDto
    {
        public int id { get; set; }
        public string type { get; set; }
    }
}
