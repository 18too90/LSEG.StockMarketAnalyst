using LSEG.StockMarketAnalyst.Domain.Models;

namespace LSEG.StockMarketAnalyst.DataSampler.Managers
{
    public interface ISampleManager
    {
        IEnumerable<StockPrice> GetSamples(IEnumerable<StockPrice> dataPoints);
    }
}