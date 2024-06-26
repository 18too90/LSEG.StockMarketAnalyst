using LSEG.StockMarketAnalyst.Domain.Models;

namespace LSEG.StockMarketAnalyst.OutlierDetector.Helpers
{
    public interface IFileHelper
    {
        void WriteFiles(KeyValuePair<string, List<StockPrice>> outliers);
    }
}