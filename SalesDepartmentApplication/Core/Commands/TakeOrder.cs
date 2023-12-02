﻿using ConsoleTables;
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
                int positionsInOrder = random.Next(1, 4);

                var maxProductId = (int)database.Products.Select(p => p.Id).Max() - 1;
                var products = await database.Products.ToArrayAsync();
                
                // генерация товара
                while (order.Count < positionsInOrder)
                {
                    var newProductId = random.Next(0, maxProductId);
                    var product = products[newProductId];

                    if (!order.Select(o => o.Product).Contains(product))
                    {
                        order.Add(new OrderPosition
                        {
                            Product = product,
                            Amount = random.Next(1, 3)
                        });
                    }
                }

                return order;
            }
        }
    }
}