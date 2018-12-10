using System.Windows;
using Tick42;

namespace Notifications
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Glue42 Glue { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Glue = new Glue42();
            Glue.Initialize("Notifications");
        }
    }
}
