using LSEG.StockMarketAnalyst.DataSampler.Helpers;
using LSEG.StockMarketAnalyst.DataSampler.Kafka;
using LSEG.StockMarketAnalyst.DataSampler.Managers;
using LSEG.StockMarketAnalyst.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LSEG.StockMarketAnalyst.DataSampler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataSamplerController : ControllerBase
    {


        private ILogger<DataSamplerController> Logger { get; }
        private ISampleManager SampleManager { get; }
        private IDirectoryHelper DirectoryHelper { get; }
        private IFileHelper FileHelper { get; }
        private IStockPriceSamplesProducer KafkaProducer { get; }
        private const string Topic_Name = "stock_price_samples";

        public DataSamplerController(ILogger<DataSamplerController> logger,
            ISampleManager sampleManager,
            IDirectoryHelper directoryHelper,
            IFileHelper fileHelper,
            IStockPriceSamplesProducer kafkaProducer)
        {
            Logger = logger;
            SampleManager = sampleManager;
            DirectoryHelper = directoryHelper;
            FileHelper = fileHelper;
            KafkaProducer = kafkaProducer;
        }

        [HttpGet(Name = "GenerateDataSamples")]
        public ActionResult DoProcess(int countOfFilesToProcessPerExchange)
        {
            try
            {
                var perExchangeFiles = DirectoryHelper.GetFiles(countOfFilesToProcessPerExchange);
                foreach (var exchange in perExchangeFiles)
                {
                    if (!string.IsNullOrEmpty(exchange.Key))
                    {
                        foreach(var file in exchange.Value)
                        {
                            var fileName = Path.GetFileName(file).Replace(".csv", "") ;
                            var records = FileHelper.GetRecords(file);
                            if (records == null || !records.Any()) continue;
                            var samples = SampleManager.GetSamples(records);
                            if(samples == null || !samples.Any()) continue;
                            KafkaProducer.Produce(Topic_Name, $"{exchange.Key}_{fileName}", samples.ToList());
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }
            return Empty;
        }
    }
}
