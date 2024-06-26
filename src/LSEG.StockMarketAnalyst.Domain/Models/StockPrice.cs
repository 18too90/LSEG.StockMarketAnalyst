using CsvHelper.Configuration.Attributes;

namespace LSEG.StockMarketAnalyst.Domain.Models
{
    [CultureInfo("InvariantCulture")]
    public class StockPrice
    {
        public string? Id { get; set; }
        public string? Timestamp { get; set; }
        public decimal Price { get; set; }
    }
}
