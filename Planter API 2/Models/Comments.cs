using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Planter_API_2.Models
{
    public class Comments
    {
        [Key]
        public int CommentID { get; set; }

        public Article Article { get; set; }
        [ForeignKey("Article")]
        public int FK_ArticleID { get; set; }

        public string Note { get; set; }
    }
}
