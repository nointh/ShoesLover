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
        public string Reason { get; set; }
        public int Status { get; set; }
        public double Coupon { get; set; }
        public double Total { get; set; }
        public int PaymentID { get; set; }
    }
}
