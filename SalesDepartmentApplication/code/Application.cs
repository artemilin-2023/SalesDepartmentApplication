using SalesDepartmentApplication.Core.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SalesDepartmentApplication.Core
{
    internal static class Application
    {
        private const int TimeBeforeContinuing = 600;

        public static async Task RunAsync()
        {
            while (true)
            {
                var currentCommand = SelectCommand();
                await currentCommand.ExecuteAsync();

                await Console.Out.WriteLineAsync("Для продолжения нажмите Enter...");
                Console.Read();
            }
        }

        private static ICommand SelectCommand()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(
                    "Список команд:\n" +
                    "1 - вывести католог товаров\n" +
                    "2 - товары на складе\n" +
                    "3 - принять заказ\n" +
                    "4 - закрыть программу\n");

                Console.Write("Выберите команду: ");
                var userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        return new PrintCatalog();

                    case "2":
                        return new PrintProductsInWarehouse();

                    case "3":
                        return new TakeOrder();

                    case "4":
                        return new CloseProgram();

                    default:
                        Console.WriteLine("Команда не распознана");
                        Thread.Sleep(TimeBeforeContinuing);
                        break;
                }
            }
        }
    }
}
