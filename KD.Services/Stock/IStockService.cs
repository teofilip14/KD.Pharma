using KD.Common.Model.Models;

namespace KD.Services.Stock
{
    public interface IStockService
    {
        IEnumerable<StockModel> GetStocks();
        StockModel GetStockById(Guid stockId);
        void DeleteStock(Guid stockId);
        StockModel UpdateStock(StockModel stock);
        StockModel CreateStock(StockModel stock);
    }
}
