using LSEG.StockMarketAnalyst.Domain.Models;

namespace LSEG.StockMarketAnalyst.OutlierDetector.Helpers
{
    public class StatisticsHelper : IStatisticsHelper
    {
        public StatisticsHelper() 
        {

        }   

        public decimal Mean (List<StockPrice> sampleData) 
        {
            decimal sum = 0;
            foreach (var dataPoint in sampleData)
            {
                sum += dataPoint.Price;
            }
            return sum / sampleData.Count;
        }


        /// <summary>
        /// Calculate std deviation
        /// </summary>
        /// <see cref="https://www.geeksforgeeks.org/standard-deviation-formula/"/>
        /// <param name="price"></param>
        /// <returns></returns>
        public decimal StdDev (List<StockPrice> sampleData, decimal mean) 
        {
            var varianceSqrSum = 0M;
            var count = sampleData.Count;
            foreach(var dataPoint in sampleData)
            {
                var variance = (dataPoint.Price - mean);
                var varSqr = variance * variance;
                varianceSqrSum += varSqr;
            }

            return (decimal)Math.Sqrt((double)(varianceSqrSum / (count - 1)));
        }

        public decimal Threshhold (decimal stdDev) 
        {
            return 2 * stdDev;   
        }

    }
}
