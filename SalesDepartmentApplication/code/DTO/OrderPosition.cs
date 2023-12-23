using SalesDepartmentApplication.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDepartmentApplication.Core.DTO
{
    internal class OrderPosition
    {
        public Product Product {  get; set; }
        public long Amount { get; set; }
        public double TotalPrice { get => Product.Price * Amount; }
    }
}
