using SalesDepartmentApplication.Core.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SalesDepartmentApplication.Core
{
    internal class CloseProgram : ICommand
    {
        public async Task ExecuteAsync()
        {
            await Console.Out.WriteLineAsync("Программа завершена и сейчас закроется...");
            Thread.Sleep(1000);
            Environment.Exit(0);
        }
    }
}