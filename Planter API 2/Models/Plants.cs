using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Planter_API_2.Models
{
    public class Plants
    {
        [Key]
        public int PlantID { get; set; }
        public string PlantName { get; set; }

        //Foreign Keys
        public PlantType PlantType { get; set; }
        [ForeignKey("PlantType")]
        public int PlantType_ID { get; set; }

        public Climates Climates { get; set; }
        [ForeignKey("Climates")]
        public int Climate_ID { get; set; }

        public Users Users { get; set; }
        [ForeignKey("Users")]
        public int UserID { get; set; }

        public Edible Edible { get; set; }
        [ForeignKey("Edible")]
        public int EdibleID { get; set; }

        public ApprovedType ApprovedType { get; set; }
        [ForeignKey("ApprovedType")]
        public int ApprovedTypeID { get; set; }

        //Image Senere
    }
}
