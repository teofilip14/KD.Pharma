using KD.Core.Data;
using System;
using System.Collections.Generic;

namespace KD.Core.DomainModels
{
    public partial class Product : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Price { get; set; }
        public string Producer { get; set; } = null!;

        public virtual Stock Stock { get; set; } = null!;
    }
}
