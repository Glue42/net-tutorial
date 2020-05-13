using GnsDesktopManager.Model;
using System;
using System.Windows;
using Tick42.Windows;

namespace Notifications
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RaiseButton.Click += RaiseButton_Click;
            RegisterToGlueWindows();
        }

        private void RegisterToGlueWindows()
        {
            //  1. Try to get startup options passed from GD. Create our default options if there aren't any passed options. Make your app a Glue flat window with title Clients
            //  Hint - you can use the placement object for your default config and you can get the startup options from Glue.GlueWindows.GetStartupOptions();
            var swOptions = App.Glue.GlueWindows.GetStartupOptions();
            if (swOptions == null)
            {

                swOptions = new GlueWindowOptions();

                var bounds = new GlueWindowBounds
                {
                    Width = 300,
                    Height = 190
                };
                swOptions
                    .WithId(Guid.NewGuid().ToString())
                    .WithScreenBounds(bounds);
            }

            swOptions
                .WithType(GlueWindowType.Flat)
                .WithTitle("Notifications");

            App.Glue.GlueWindows.RegisterWindow(this, swOptions);
        }

        private void RaiseButton_Click(object sender, RoutedEventArgs e)
        {
            // 4.1 Raise a notification here
            // 4.2 The notification should invoke the method registered in the Clients application which opens the Contact Info
            var notification = new DesktopNotification(NotificationTitle.Text, NotificationSeverity.Low,GlueRoutingDetailMethodName: "Clients.ShowContacts");

            App.Glue.Notifications.Publish(notification);
        }
    }
}
