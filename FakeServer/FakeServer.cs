namespace FakeServer
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore;

    public static class FakeServer
    {
        public static void StartServer(string url)
        {
            CreateWebHostBuilder().Start(url);
        }

        static IWebHostBuilder CreateWebHostBuilder() =>
            WebHost.CreateDefaultBuilder().UseStartup<Startup>();
    }
}