using ConsoleTables;
using Microsoft.EntityFrameworkCore;
using SalesDepartmentApplication.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDepartmentApplication.Core
{
    internal class PrintProductsInWarehouse : ICommand
    {
        public async Task ExecuteAsync()
        {
            Console.Clear();
            await Console.Out.WriteLineAsync("Запрос выполняется...");

            using(var database = new DataContext())
            {
                await PrintProductsInStock(database);
                await PrintMissingItems(database);
            }
        }

        private async Task PrintProductsInStock(DataContext database)
        {
            foreach (var warehouse in database.Warehouses)
            {
                await Console.Out.WriteLineAsync($"Товары, находящиеся по адресу {warehouse.Address}:");
                var table = new ConsoleTable("Id товара", "Название", "Колличество", "Позиция на складе (id ячейки)");

                var cells = database.Cells.Where(c => c.WarehouseId == warehouse.Id);
                foreach (var cell in cells)
                {
                    var product = await database.Products.FirstAsync(p => p.Id == cell.ProductId);
                    table.AddRow(product.Id, product.Name, cell.Amount, cell.Id);
                }

                await Console.Out.WriteLineAsync(table.ToStringAlternative());
            }
        }

        private async Task PrintMissingItems(DataContext database)
        {
            await Console.Out.WriteLineAsync($"Товары, которых нет в наличии:");
            var table = new ConsoleTable("Id товара", "Название");

            var productsInStock = database.Cells.Select(c => c.ProductId).ToHashSet();
            foreach (var product in database.Products)
            {
                if (!productsInStock.Contains(product.Id))
                {
                    table.AddRow(product.Id, product.Name);
                }
            }

            await Console.Out.WriteLineAsync(table.ToStringAlternative());
        }
    }
}