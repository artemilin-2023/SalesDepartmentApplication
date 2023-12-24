global using SalesDepartmentApplication.Helpers;
using Microsoft.Extensions.Logging;
using SalesDepartmentApplication.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDepartmentApplication
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                await Application.RunAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }
    }
}
