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
        [Required]
        public string PlantName { get; set; }

        //Foreign Keys
        public PlantType PlantType { get; set; }
        [ForeignKey("PlantType")]
        public int FK_PlantTypeID { get; set; }

        public Climates Climates { get; set; }
        [ForeignKey("Climates")]
        public int FK_ClimateID { get; set; }

        public Users Users { get; set; }
        [ForeignKey("Users")]
        public int FK_UserID { get; set; }

        public Edible Edible { get; set; }
        [ForeignKey("Edible")]
        public int FK_EdibleID { get; set; }

        public ApprovedType ApprovedType { get; set; }
        [ForeignKey("ApprovedType")]
        public int FK_ApprovedTypeID { get; set; }

        public byte[] Image { get; set; }

        public List<Article> Articles { get; set; }
        //Image Senere
    }

    [NotMapped]
    public class PlantsDto
    {
        public int id { get; set; }
        public string info { get; set; }
        public string type { get; set; }
        public string climate { get; set; }
        public string edible { get; set; }
        public string username { get; set; }
        public string approved { get; set; }
        public byte[] image { get; set; }
    }
}
