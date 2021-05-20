using Microsoft.Extensions.Hosting;

namespace agritech_hack
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .Build();

            host.Run();
        }
    }
}