using DOT.AGM.Transport;
using System;
using System.Windows;
using System.Windows.Media;
using Tick42.Contexts;
using Tick42.StickyWindows;

namespace Portfolio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataService dataProvider;

        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            App.Glue.Interop.ConnectionStatusChanged += Interop_ConnectionStatusChanged;
            dataProvider = new DataService();
            // 3.1 Interop data transfer
            RegisterInteropMethod(dataProvider);
            // 2. Contexts data transfer
            //SubscribeForContextUpdate();
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
            // 3. Make your app a sticky flat window with title Portfolio
            swOptions
                .WithType(SwWindowType.Flat)
                .WithTitle("Portfolio");

            App.Glue.StickyWindows.RegisterWindow(this, swOptions);
        }

        private void Interop_ConnectionStatusChanged(object sender, Tick42.InteropStatusEventArgs e)
        {
            var color = e.Status.State == TransportState.Connected ? Colors.ForestGreen : Color.FromRgb(205, 0, 0);

            GlueConnectionsStatusBorder.Background = new SolidColorBrush(color);
            GlueConnectionsStatus.Content = $"Glue is {e.Status.State}";
        }

        private void SubscribeForContextUpdate()
        {
            // 2. Subscribe for updates for the context which is updated from the Clients app
            App.Glue.Contexts.GetContext<IPartyDetailsContext>("CurrentParty").ContinueWith((partyContext) =>
            {
                var currContext = partyContext.Result;
                currContext.ContextUpdated += Context_ContextUpdated;
            });
        }

        private void RegisterInteropMethod(DataService dataProvider)
        {
            dataProvider.OnDataReceived += DataProvider_OnDataReceived;
            // 3.1 Register the interop service which will update the UI
            App.Glue.Interop.RegisterService<IDataService>(dataProvider);
        }

        private void Context_ContextUpdated(object sender, ContextUpdatedEventArgs e)
        {
            var newContext = sender as IPartyDetailsContext;

            Dispatcher.BeginInvoke(new Action(() =>
            {
                ClientNameLabel.Content = newContext.Name;
                DetailsGrid.ItemsSource = newContext.Details;
            }));
        }

        private void DataProvider_OnDataReceived(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                ClientNameLabel.Content = dataProvider.CurrentClientName;
                DetailsGrid.ItemsSource = dataProvider.CurrenData;
            }));
        }
    }
}
