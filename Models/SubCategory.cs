using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Models
{
    public class SubCategory
    {
        public int  Id { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public bool Active { get; set; }
    }
}
