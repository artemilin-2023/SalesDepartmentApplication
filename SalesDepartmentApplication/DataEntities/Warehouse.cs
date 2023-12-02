using System;
using System.Collections.Generic;

namespace SalesDepartmentApplication.DataEntities;

public partial class Warehouse
{
    public long Id { get; set; }

    public string Address { get; set; } = null!;

    public virtual ICollection<Cell> Cells { get; } = new List<Cell>();
}
