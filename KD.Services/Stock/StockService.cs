using KD.Common.Model.Automapper;
using KD.Common.Model.Models;
using KD.Core.Data;

namespace KD.Services.Stock
{
    public class StockService : IStockService
    {
        #region Fields
        private readonly IRepository<Core.DomainModels.Stock> stockRepository;

        public StockService(IRepository<Core.DomainModels.Stock> stockRepository)
        {
            this.stockRepository = stockRepository;
        }
        #endregion
        #region Methods

        public IEnumerable<StockModel> GetStocks()
        {
            var stocks = stockRepository.Table.Select(x => x.ToModel()).ToList();
            return stocks;
        }

        public StockModel CreateStock(StockModel stock)
        {
            if (stock == null)
                return null;

            KD.Core.DomainModels.Stock stockEntity = stock.ToEntity();
            stockEntity.StockId = Guid.Empty;
            stockRepository.Insert(stockEntity);

            StockModel createdStock = GetStockById(stockEntity.StockId);
            return createdStock;
        }

        public StockModel GetStockById(Guid stockId)
        {
            var stocks = stockRepository.Table.FirstOrDefault(s => s.StockId == stockId);
            return stocks.ToModel();
        }

        public StockModel UpdateStock(StockModel stock)
        {
            if (stock == null) return null;
            var databaseEntity = stockRepository.TableNoTracking.FirstOrDefault(s => s.StockId == stock.StockId);
            if (databaseEntity == null)
                return null;

            stockRepository.Update(stock.ToEntity());
            return GetStockById(stock.StockId);
        }

        public void DeleteStock(Guid stockId)
        {
            var databaseEntity = stockRepository.Table.FirstOrDefault(s => s.StockId == stockId);
            if (databaseEntity == null)
                return;

            stockRepository.Delete(databaseEntity);
        }

        #endregion
    }
}
