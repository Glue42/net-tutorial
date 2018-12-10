using DOT.AGM.Transport;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Tick42.StickyWindows;

namespace Clients
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            ClientsList.ItemsSource = DataReceiver.GetClients();
            ClientsList.MouseDoubleClick += ClientsList_MouseDoubleClick;
            App.Glue.Interop.ConnectionStatusChanged += Interop_ConnectionStatusChanged;
            // 4.2 This is an appropriate place to register your service
            App.Glue.Interop.RegisterService<IContactsService>(new ContactsService());
            RegisterToStickyWindows();
        }

        private void RegisterToStickyWindows()
        {
            // 1. Try to get startup options passed from GD
            var swOptions = App.Glue.StickyWindows.GetStartupOptions();
            if (swOptions == null)
            {
                // 2. Create our default options if there aren't any passed options
                swOptions = new SwOptions();
                var placement = new SwScreenPlacement();
                var bounds = new SwBounds
                {
                    Width = 800,
                    Height = 450
                };
                placement.WithBounds(bounds);
                swOptions
                    .WithId(Guid.NewGuid().ToString())
                    .WithPlacement(placement);
            }
            // 3. Make your app a sticky flat window with title Clients
            swOptions
                .WithType(SwWindowType.Flat)
                .WithTitle("Clients");

            App.Glue.StickyWindows.RegisterWindow(this, swOptions);
        }

        private void Interop_ConnectionStatusChanged(object sender, Tick42.InteropStatusEventArgs e)
        {
            // Responsible for the Glue status label
            var color = e.Status.State == TransportState.Connected ? Colors.ForestGreen : Color.FromRgb(205, 0, 0);

            GlueConnectionsStatusBorder.Background = new SolidColorBrush(color);
            GlueConnectionsStatus.Content = $"Glue is {e.Status.State}";
        }

        private void ClientsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var firstCell = ClientsList.SelectedCells.FirstOrDefault();

            var selectedClient = firstCell.Item as Client;
            if (selectedClient != null)
            {
                var data = selectedClient.Details;

                // 2. Context update should be invoked here
                // ContextUpdate(selectedClient.FullName, data);
                // 3.2 InteropCall should be invoked here
                InteropCall(selectedClient.FullName, data);

            }
        }

        private void InteropCall(string currClientName, PortfolioDetails[] data)
        {
            // 3.2 Invoke here the method registered in Portfolio with the data from the clicked row
            IDataService dataServiceProxy = App.Glue.Interop.CreateServiceProxy<IDataService>();
            if (App.Glue.Interop.IsServiceAvailable(dataServiceProxy))
            {
                dataServiceProxy.PopulateData(currClientName, data.ToList());
            }
        }

        private void ContextUpdate(string clientName, PortfolioDetails[] data)
        {
            // 2. Update the context here with the data from the clicked row 
            // *Hint* There is a IPartyDetailsContext which extends IContext in the Portfolio application
            App.Glue.Contexts.GetContext("CurrentParty").ContinueWith((contextTask) =>
            {
                var context = contextTask.Result;

                context.Set(new { Name = clientName, Details = data });
            });

        }
    }
}
