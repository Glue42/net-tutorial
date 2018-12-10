using DOT.AGM.Client;
using DOT.AGM.Services;
using System.Collections.Generic;

namespace Clients
{
    
    [ServiceContract(MethodNamespace = "Clients.")]
    public interface IContactsService
    {

        //4.2 Add ShowContacts here to register it
        [ServiceOperation(AsyncIfPossible = true, ExceptionSafe = true)]
        void ShowContacts();

        // 5. Add FindWhoToCall method which return a collection with contact information 
        [ServiceOperation(InvocationTargetType = MethodTargetType.Any)]
        List<ContactInfo> FindWhoToCall();
    }
}
