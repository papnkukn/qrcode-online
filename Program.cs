using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Linq;
using System.Reflection;

namespace QRCode
{
    public class Program
    {
        public static string Host = null;
        public static int? Port = null;

        public static void Version()
        {
            var assembly = Assembly.GetEntryAssembly();
            var attribute = assembly.GetCustomAttributes(false).OfType<AssemblyInformationalVersionAttribute>().First() as AssemblyInformationalVersionAttribute;
            Console.WriteLine(attribute.InformationalVersion);
        }

        public static void Help()
        {
            Console.WriteLine("Mestric web server");
            Console.WriteLine("");
            Console.WriteLine("Usage:");
            Console.WriteLine("  dotnet QRCode.dll [options]");
            Console.WriteLine("");
            Console.WriteLine("Options:");
            Console.WriteLine("  --help            Print this message");
            Console.WriteLine("  --version         Print version info");
            Console.WriteLine("  --host [host]     HTTP server hostname");
            Console.WriteLine("  --port [port]     HTTP server port number");
        }

        public static void Main(string[] args)
        {
            //Parse command line arguments
            if (args != null && args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        case "--help":
                            Help();
                            Environment.Exit(0);
                            return;

                        case "--version":
                            Version();
                            Environment.Exit(0);
                            return;

                        case "--host":
                            Host = args[++i];
                            break;

                        case "--port":
                            Port = int.Parse(args[++i]);
                            break;

                        default:
                            throw new ArgumentException("Unknown argument: " + args[i]);
                    }
                }
            }

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var webhost = WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
#if DEBUG
            return webhost;
#endif

            string url = "http://" + (Host ?? "0.0.0.0") + ":" + (Port ?? 3000);
            return webhost.UseUrls(url);
        }
    }
}
