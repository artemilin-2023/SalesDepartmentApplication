using ConsoleTables;
using Microsoft.EntityFrameworkCore;
using SalesDepartmentApplication.Core.Commands;
using SalesDepartmentApplication.Core.DTO;
using SalesDepartmentApplication.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDepartmentApplication.Core
{
    internal class TakeOrder : ICommand
    {
        private const int MaxPositionsInOrder = 4;
        private const int MaxAmount = 3;

        public async Task ExecuteAsync()
        {
            Console.Clear();
            await Console.Out.WriteLineAsync($"Заказчик: Иванов Иван Иванович\nДата оформления заказа: {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}\nПозиции в заказе:");

            var table = new ConsoleTable("Название товара", "Колличество (шт)", "Цена за шт. (руб)", "Итого");
            var order = await CreateOrder();
            
            foreach (var orderPosition in order)
            {
                table.AddRow(orderPosition.Product.Name, orderPosition.Amount, orderPosition.Product.Price, orderPosition.TotalPrice);
            }

            if (order.Count > 1)
                table.AddRow("", "", "", order.Select(o => o.TotalPrice).Sum());

            await Console.Out.WriteLineAsync(table.ToStringAlternative());
        }

        private async Task<List<OrderPosition>> CreateOrder()
        {
            using (var database = new DataContext())
            {
                var order = new List<OrderPosition>();

                var random = new Random();
                int positionsInOrder = random.Next(1, MaxPositionsInOrder);

                var products = GetProducts(database);
                
                // генерация товара
                while (order.Count < positionsInOrder)
                {
                    var newProductId = random.Next(0, products.Count);
                    var product = products[newProductId];

                    if (!order.Select(o => o.Product).Contains(product))
                    {
                        order.Add(new OrderPosition
                        {
                            Product = product,
                            Amount = random.Next(1, MaxAmount)
                        });
                    }
                }

                return order;
            }
        }

        private List<Product> GetProducts(DataContext database)
        {
            var result = new List<Product>();

            foreach (var warehouse in database.Warehouses)
            {
                var productsId = database.Cells.Where(c => c.WarehouseId == warehouse.Id).Select(c => c.ProductId).ToHashSet();
                var products = database.Products.Where(p => productsId.Contains(p.Id));
                result.AddRange(products);
            }

            return result;
        }
    }
}