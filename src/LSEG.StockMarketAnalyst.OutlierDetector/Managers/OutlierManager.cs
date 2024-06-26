using LSEG.StockMarketAnalyst.Domain.Models;
using LSEG.StockMarketAnalyst.OutlierDetector.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LSEG.StockMarketAnalyst.OutlierDetector.Managers
{
    public class OutlierManager : IOutlierManager
    {
        private IStatisticsHelper StatisticsHelper { get; }
        public OutlierManager(IStatisticsHelper statisticsHelper)
        {
            StatisticsHelper = statisticsHelper;
        }

        public IEnumerable<StockPrice> FindOutliers(List<StockPrice> dataSample)
        {
            var mean = StatisticsHelper.Mean(dataSample);
            var stdDev = StatisticsHelper.StdDev(dataSample, mean);
            var threshold = StatisticsHelper.Threshhold(stdDev);
            foreach (StockPrice dataPoint in dataSample)
            {
                var variance = Math.Abs(dataPoint.Price - mean);
                if (variance >= threshold)
                {
                    yield return dataPoint;
                }
            }
        }
    }
}
