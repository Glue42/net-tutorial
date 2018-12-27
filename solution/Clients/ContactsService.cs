using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tick42.AppManager;

namespace Clients
{
    public class ContactsService : IContactsService
    {
        public void ShowContacts()
        {
            // 5. Start the Contacts app
            var detailsApp = App.Glue.AppManager.Applications.FirstOrDefault((app) => app.Name == "Contacts");
            var context = AppManagerContext.CreateNew();

            detailsApp?.Start(context).Wait();
        }

        public List<ContactInfo> FindWhoToCall()
        {
            // 5. Return the contact information collection
            var contacts = DataReceiver.GetClients().Select(client => new ContactInfo() { FullName = client.FullName, PhoneNumber = client.PhoneNumber });
            return contacts.ToList(); ;
        }

    }
}
