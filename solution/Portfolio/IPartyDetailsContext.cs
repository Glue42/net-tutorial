using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tick42.Contexts;

namespace Portfolio
{
    // 2. Interface which will provide a type for the context
    public interface IPartyDetailsContext : IContext
    {
        string Name { get; set; }
        PortfolioDetails[] Details { get; set; }
    }
}
