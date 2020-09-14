using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Planter_API_2.Models
{
    public class PlantType
    {
        [Key]
        public int PlantTypeID { get; set; }
        [Required]
        public string PType { get; set; }

        //Relationships
        public List<Plants> Plants { get; set; }
    }

    [NotMapped]
    public class PlantTypeDto
    {
        public int id { get; set; }
        public string info { get; set; }
    }
}
