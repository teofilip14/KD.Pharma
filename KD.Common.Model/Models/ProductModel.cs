using KD.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD.Common.Model.Models
{
    public class ProductModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Price { get; set; }
        public string Producer { get; set; } = null!;

        //public virtual Stock Stock { get; set; } = null!;
        public virtual StockModel? Stock { get; set; }
    }
}
