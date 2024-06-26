using LSEG.StockMarketAnalyst.Domain.Models;

namespace LSEG.StockMarketAnalyst.DataSampler.Kafka
{
    public interface IStockPriceSamplesProducer
    {
        void Produce(string topicName, string key, List<StockPrice> data);
    }
}