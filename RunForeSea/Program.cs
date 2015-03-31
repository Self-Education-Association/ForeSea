using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Configuration;

namespace RunForeSea
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    return;
                }
                switch (args[0])
                {
                    case "CheckIn":
                        System.Diagnostics.Process.Start(checkIn);
                        break;
                    case "Query":
                        System.Diagnostics.Process.Start(query);
                        break;
                    default:
                        Console.WriteLine("Error Input Data!");
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {

            }
        }
        public static string checkIn = ConfigurationManager.AppSettings["CheckIn"];
        public static string query = ConfigurationManager.AppSettings["Query"];
    }
}
