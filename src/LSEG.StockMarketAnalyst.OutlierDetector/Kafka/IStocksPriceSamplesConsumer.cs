using LSEG.StockMarketAnalyst.Domain.Models;

namespace LSEG.StockMarketAnalyst.OutlierDetector.Kafka
{
    public interface IStocksPriceSamplesConsumer
    {
        void Consume(string topicName, CancellationToken cancellation = default);
    }
}