using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebAPISYS
{
    /// <summary>
    /// Program class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main function
        /// </summary>
        /// <param name="args">the args</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Create web host builder
        /// </summary>
        /// <param name="args">the args</param>
        /// <returns>a Webhostbuilder</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
        }
    }
}
