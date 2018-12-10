using Tick42;

namespace Notifications
{
    public class Glue
    {
        public static void Init()
        {
            var applicationName = "Notifications";
            Glue42 = new Glue42();
            Glue42.Initialize(applicationName);
        }

        public static Glue42 Glue42 { get; private set; }
    }
}
