using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Planter_API_2.Models
{
    public class Article
    {
        public int ArticleID { get; set; }
        public string Text { get; set; }
        public string Tips { get; set; }

        public int ApprovedTypeID { get; set; }
        public ApprovedType ApprovedType { get; set; }

        public int PlantsID { get; set; }
        public Plants Plants { get; set; }

        public List<Comments> Comments { get; set; }
        //[Key]
        //public int ArticleID { get; set; }//PK

        //[Required]
        //public string Text { get; set; }
        //public string Tips { get; set; }

        ////FK ApprovedTypeID
        //public ApprovedType ApprovedType { get; set; }
        //[ForeignKey("ApprovedType")]
        //public int FK_ApprovedTypeID { get; set; }

        ////FK PlantID
        //[ForeignKey("Plants")]
        //public int FK_PlantsID;
        //public Plants Plants { get; set; }

        ////Relationships
        ////PK ArticleID(Article) TO FK_ArticleID(Comments)
        //public List<Comments> Comments { get; set; }
    }
}
