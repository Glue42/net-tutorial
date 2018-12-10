
namespace Clients
{
    public class Client
    {
        public string FullName { get; set; }
        public string PID { get; set; }
        public string GID { get; set; }
        public string Manager { get; set; }
        public PortfolioDetails[] Details{ get; set; }
        public string PhoneNumber { get; set; }
    }
}
