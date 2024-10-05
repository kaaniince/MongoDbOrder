using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbOrder.Entities
{
    public class Order
    {
        public string OrderId { get; set; }
        public string ClientName { get; set; }
        public string TownName { get; set; }
        public string CityName { get; set; }
        public decimal TotalPrice { get; set;}
    }
}
