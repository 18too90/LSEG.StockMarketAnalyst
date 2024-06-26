using LSEG.StockMarketAnalyst.Domain.Models;

namespace LSEG.StockMarketAnalyst.OutlierDetector.Managers
{
    public interface IOutlierManager
    {
        IEnumerable<StockPrice> FindOutliers(List<StockPrice> dataSample);
    }
}