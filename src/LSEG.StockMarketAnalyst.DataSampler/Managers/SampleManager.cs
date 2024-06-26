using LSEG.StockMarketAnalyst.Domain.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LSEG.StockMarketAnalyst.DataSampler.Managers
{
    public class SampleManager : ISampleManager
    {
        public SampleManager() { }

        /// <summary>
        /// Get samples from data set
        /// </summary>
        /// <param name="dataPoints"></param>
        /// <returns></returns>
        public IEnumerable<StockPrice> GetSamples(IEnumerable<StockPrice> dataPoints)
        {
            var dataPointsList = dataPoints?.ToList();
            if (dataPointsList == null || dataPointsList.Count == 0 || dataPointsList.Count < 30)
                throw new ArgumentException("Not enough data points in the file.");

            var random = new Random();
            // starting point can be uptill 30th record from last
            int startingPoint = random.Next(0, dataPointsList.Count - 29); 
            return dataPointsList.GetRange(startingPoint, 30);
        }
    }
}
