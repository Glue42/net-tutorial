using DOT.AGM.Transport;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Tick42.Windows;

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
            RegisterToGlueWindows();
        }

        private void RegisterToGlueWindows()
        {
            // 1. Try to get startup options passed from GD
            // Create our default options if there aren't any passed options
            // Make your app a Glue flat window with title Clients

            var bounds = new GlueWindowBounds
            {
                Width = 800,
                Height = 450
            };
            var id = Guid.NewGuid().ToString();
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
                // 3.2 InteropCall should be invoked here

            }
        }

        private void InteropCall(string currClientName, PortfolioDetails[] data)
        {
            // 3.2 Invoke here the method registered in Portfolio with the data from the clicked row
        }

        private void ContextUpdate(string clientName, PortfolioDetails[] data)
        {
            // 2. Update the context here with the data from the clicked row 
            // *Hint* There is a IPartyDetailsContext which extends IContext in the Portfolio application
        }
    }
}
