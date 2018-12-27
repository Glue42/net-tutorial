using DOT.AGM.Services;
using System;
using System.Collections.Generic;

namespace Clients
{
    //3.2 You should pass this interface to your service proxy. Keep in mind that this interface should be the same as the IDataService one in Portfolio
    [ServiceContract(MethodNamespace = "Portfolio.")]
    public interface IDataService : IDisposable
    {
    }
}
