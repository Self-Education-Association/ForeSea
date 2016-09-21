/*
Copyright [2016] [puyang.c@foxmail.com]

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
 */

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
