using Tick42;

namespace NotificationDetails
{
    public class Glue
    {
        public static void Init()
        {
            var applicationName = "NotificationDetails";
            Glue42 = new Glue42();
            Glue42.Initialize(applicationName);
        }

        public static Glue42 Glue42 { get; private set; }
    }
}
