using ConsoleTables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDepartmentApplication.Core.Commands
{
    internal class PrintCatalog : ICommand
    {
        public async Task ExecuteAsync()
        {
            Console.Clear();
            Console.WriteLine("Запрос выполняется...");

            var table = new ConsoleTable("Название", "Описание", "Цена (руб)");

            using (var database = new DataContext())
            {
                foreach (var product in database.Products)
                {
                    table.AddRow(product.Name, product?.Description, product?.Price);
                }
            }

            Console.Clear();
            await Console.Out.WriteLineAsync(table.ToStringAlternative());
        }
    }
}