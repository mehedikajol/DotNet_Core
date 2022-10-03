using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Domain.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderRef { get; set; }
        public string Address01 { get; set; }
        public string Address02 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
