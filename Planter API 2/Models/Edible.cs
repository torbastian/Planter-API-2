using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Planter_API_2.Models
{
    public class Edible
    {
        [Key]
        public int EdibleID { get; set; }

        [Required]
        public string EdibleS { get; set; }

        //Relationships
        public List<Plants> Plants { get; set; }
    }

    [NotMapped]
    public class EdibleDto
    {
        public int id { get; set; }
        public string info { get; set; }
    }
}
