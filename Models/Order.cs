using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int UID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int Total { get; set; }
    }
}
