using KD.Core.Data;
using System;
using System.Collections.Generic;

namespace KD.Core.DomainModels
{
    public partial class Stock : BaseEntity
    {
        public Guid StockId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
