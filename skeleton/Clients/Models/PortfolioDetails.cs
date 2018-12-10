using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients
{
    public class PortfolioDetails
    {
        public string Symbol { get; set; }
        public string Description { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
    }
}
