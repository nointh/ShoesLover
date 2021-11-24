using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string ColorName { get; set; }
        public string ColorImage { get; set; }
        public bool Active { get; set; }
    }
}
