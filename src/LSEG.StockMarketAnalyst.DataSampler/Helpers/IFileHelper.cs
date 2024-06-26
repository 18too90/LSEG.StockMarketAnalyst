
using LSEG.StockMarketAnalyst.Domain.Models;

namespace LSEG.StockMarketAnalyst.DataSampler.Helpers
{
    public interface IFileHelper
    {
        IEnumerable<StockPrice> GetRecords(string file);
    }
}