
namespace LSEG.StockMarketAnalyst.DataSampler.Helpers
{
    public interface IDirectoryHelper
    {
        IEnumerable<KeyValuePair<string, IEnumerable<string>>> GetFiles(int countPerExchange = 1);
    }
}