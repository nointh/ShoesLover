using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Models
{
    public class CommentAdminObject:Comment
    {
        public List<Comment> listparentcomment;
          public string ProductName;
    }
}
