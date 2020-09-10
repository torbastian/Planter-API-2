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

        [Required]
        public string Text { get; set; }
        public string Tips { get; set; }

        public int ApprovedTypeID { get; set; }
        public ApprovedType ApprovedType { get; set; }

        public int PlantsID { get; set; }
        public Plants Plants { get; set; }

        public List<Comments> Comments { get; set; }
    }
}
