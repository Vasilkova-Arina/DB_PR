using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR32
{
    public class OrderRow
    {
        public int OrderID { get; set; }
        public string Date { get; set; }
        public string Delivery { get; set; }
        public string Client { get; set; }
        public string Pickup { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public decimal DiscountPercent { get; set; }
        public bool AllInStock { get; set; }
    }
}
