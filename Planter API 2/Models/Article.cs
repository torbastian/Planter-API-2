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
        [Key]
        public int ArticleID { get; set; }//PK
        public string Text { get; set; }
        public string Tips { get; set; }

        //FK ApprovedTypeID
        public ApprovedType ApprovedType { get; set; }
        [ForeignKey("ApprovedType")]
        public int FK_ApprovedTypeID { get; set; }

        //FK PlantID
        [ForeignKey("Plants")]
        public int FK_PlantsID;
        public Plants Plants { get; set; }

        //Relationships
        //PK ArticleID(Article) TO FK_ArticleID(Comments)
        public List<Comments> Comments { get; set; }
    }
}
