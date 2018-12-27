using DOT.AGM.Client;
using DOT.AGM.Services;
using System;
using System.Collections.Generic;

namespace Contacts
{
    [ServiceContract(MethodNamespace = "Clients.")]
    public interface IContactsService : IDisposable
    {
        [ServiceOperation(InvocationTargetType = MethodTargetType.Any)]
        List<ContactInfo> FindWhoToCall();
    }
}
