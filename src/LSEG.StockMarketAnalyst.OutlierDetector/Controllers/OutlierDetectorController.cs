using LSEG.StockMarketAnalyst.OutlierDetector.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace LSEG.StockMarketAnalyst.OutlierDetector.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OutlierDetectorController : ControllerBase
    {

        private readonly ILogger<OutlierDetectorController> _logger;
        private IStocksPriceSamplesConsumer StocksPriceSamplesConsumer { get; }
        private const string Topic_Name = "stock_price_samples";

        public OutlierDetectorController(ILogger<OutlierDetectorController> logger, IStocksPriceSamplesConsumer stocksPriceSamplesConsumer)
        {
            _logger = logger;
            StocksPriceSamplesConsumer = stocksPriceSamplesConsumer;
        }

        [HttpGet(Name = "FindOutliers")]
        public ActionResult Find()
        {
            StocksPriceSamplesConsumer.Consume(Topic_Name);
            return Empty;
        }
    }
}
