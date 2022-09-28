using KD.Core.DomainModels;

namespace KD.Common.Model.Models
{
    public class StockModel
    {
        public Guid StockId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        // public virtual Product Product { get; set; } = null!;
        public virtual ProductModel? Product { get; set; }
    }
}
