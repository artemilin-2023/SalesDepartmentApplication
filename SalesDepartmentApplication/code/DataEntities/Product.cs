using System;
using System.Collections.Generic;

namespace SalesDepartmentApplication.DataEntities;

public partial class Product
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public double Price { get; set; }

    public virtual ICollection<Cell> Cells { get; } = new List<Cell>();
}
