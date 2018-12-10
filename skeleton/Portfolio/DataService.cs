using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Portfolio
{
    public class DataService : IDataService
    {
        public IEnumerable<PortfolioDetails> CurrenData { get; private set; }
        public string CurrentClientName { get; private set; }

        // 3.1 It notifies the code behind to update the UI
        public event EventHandler<EventArgs> OnDataReceived;

        public void PopulateData(string currClientName, List<PortfolioDetails> portfolioDetails)
        {
            // 3.1 Update the 2 fields and trigger the event when this method is invoked
        }

    }
}
