using DOT.AGM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio
{
    // 3.1 The class which will have the method that updates the UI
    [ServiceContract(MethodNamespace ="Portfolio.")]
    public interface IDataService
    {
        // 3.1 Add the method's signiture here 
        [ServiceOperation(AsyncIfPossible = true, ExceptionSafe =true)]
        void PopulateData(string currClientName,List<PortfolioDetails> portfolioDetails);
    }
}
