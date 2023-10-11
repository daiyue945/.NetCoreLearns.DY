using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 一对一
{//快递单
    class Delivery
    {
        public long Id { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public Order Order{ get; set; }
        public long OrderId { get; set; }

    }
}
