using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Tick42.StickyWindows;

namespace Contacts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RegisterToStickyWindows();
            ContactInfoGrid.ItemsSource = GetContacts();
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
                    Width = 510,
                    Height = 450
                };
                placement.WithBounds(bounds);
                swOptions
                    .WithId(Guid.NewGuid().ToString())
                    .WithPlacement(placement);
            }
            // 3. Make your app a sticky flat window with title Contacts
            swOptions
                .WithType(SwWindowType.Flat)
                .WithTitle("Contacts");

            App.Glue.StickyWindows.RegisterWindow(this, swOptions);
        }

        private IEnumerable<ContactInfo> GetContacts()
        {
            IEnumerable<ContactInfo> contacts = new ObservableCollection<ContactInfo>();
            // 5 Invoke the FindWhoToCall method and use the returned value to populate the grid
            var contactsProxy = App.Glue.Interop.CreateServiceProxy<IContactsService>();

            if (App.Glue.Interop.IsServiceAvailable(contactsProxy))
            {
                contacts = contactsProxy.FindWhoToCall();
            }

            return contacts;
        }

    }
}
