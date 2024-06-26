using LSEG.StockMarketAnalyst.Domain.Models;

namespace LSEG.StockMarketAnalyst.OutlierDetector.Helpers
{
    public interface IStatisticsHelper
    {
        decimal Mean(List<StockPrice> sampleData);
        decimal StdDev(List<StockPrice> sampleData, decimal mean);
        decimal Threshhold(decimal stdDev);
    }
}