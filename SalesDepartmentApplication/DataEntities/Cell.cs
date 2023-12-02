using System;
using System.Collections.Generic;

namespace SalesDepartmentApplication.DataEntities;

public partial class Cell
{
    public long? ProductId { get; set; }

    public long? WarehouseId { get; set; }

    public long? Amount { get; set; }

    public long Id { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Warehouse? Warehouse { get; set; }
}
