using DOT.AGM.Services;
using System;
using System.Collections.Generic;

namespace Clients
{
    //3.2 Here you should create an interface to pass to your service proxy 
    [ServiceContract(MethodNamespace = "Portfolio.")]
    public interface IDataService : IDisposable
    {
    }
}
