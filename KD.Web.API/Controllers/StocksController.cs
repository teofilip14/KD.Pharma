using KD.Common.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using KD.Services.Stock;

namespace KD.Web.API.Controllers
{
    [Authorize]
    [ApiController]    
    public class StocksController : Controller
    {
        private readonly IStockService stockService;
        public StocksController(IStockService stockService)
        {
            this.stockService = stockService;
        }

        [AllowAnonymous]
        [Route("/api/Stocks")]
        [HttpGet]
        public IEnumerable<StockModel> Get()
        {
            try
            {
                var stocks = stockService.GetStocks();
                return stocks;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [Route("/api/Stocks/{stockId}")]
        [HttpGet]
        public StockModel Get(Guid stockId)
        {
            try
            {
                var stock = stockService.GetStockById(stockId);

                return stock;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [Route("/api/Stocks/{stockId}")]
        [HttpDelete]
        public void Delete(Guid stockId)
        {
            try
            {
                stockService.DeleteStock(stockId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        //[Route("/api/Stocks")]
        //[HttpPost]
        //public StockModel Create([FromBody] StockModel stock)
        //{
        //    try
        //    {
        //        StockModel createdStock = stockService.CreateStock(stock);
        //        return createdStock;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message.ToString());
        //    }
        //}

        [Route("/api/Stocks/{stockId}")]
        [HttpPut]
        public StockModel Update(Guid stockId, [FromBody] JsonElement stock)
        {
            try
            {
                var stockModel = JsonSerializer.Deserialize<StockModel>(stock);
                StockModel updatedStock = stockService.UpdateStock(stockModel);
                return updatedStock;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}