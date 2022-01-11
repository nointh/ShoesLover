using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Models
{
    public class Rating
    {
        public int RatingID { get; set; }
        public int ProductID { get; set; }
        public int RatingPro { get; set; }
        public int ColorID { get; set; }
    }
}
