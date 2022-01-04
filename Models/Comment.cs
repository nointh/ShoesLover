using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Models
{
    public class Comment
    {

        public int ID { get; set; }
        public string CommentName { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        public int CommentProductID { get; set; }
        public int CommentStatus { get; set; }
        public int CommentColorID { get; set; }
        public int CommentParentComment { get; set; }
    }
}
