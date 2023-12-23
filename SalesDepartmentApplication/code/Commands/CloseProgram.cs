using SalesDepartmentApplication.Core.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SalesDepartmentApplication.Core
{
    internal class CloseProgram : ICommand
    {
        private const int TimeBeforeClosingMS = 1000;

        public async Task ExecuteAsync()
        {
            await Console.Out.WriteLineAsync("Программа завершена и сейчас закроется...");
            Thread.Sleep(TimeBeforeClosingMS);
            Console.Clear();

            Environment.Exit(0);
        }
    }
}