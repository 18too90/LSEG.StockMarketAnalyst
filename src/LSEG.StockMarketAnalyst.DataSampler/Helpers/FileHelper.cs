using LSEG.StockMarketAnalyst.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace LSEG.StockMarketAnalyst.DataSampler.Helpers
{
    public class FileHelper : IFileHelper
    {
        private ILogger<FileHelper> Logger { get; }
        public FileHelper(ILogger<FileHelper> logger)
        {
            Logger = logger;
        }

        public IEnumerable<StockPrice> GetRecords(string file)
        {
            var records = new List<StockPrice>();
            try
            {
                var csvConfig = CsvHelper.Configuration.CsvConfiguration.FromAttributes<StockPrice>();
                csvConfig.HasHeaderRecord = false;
                csvConfig.IgnoreBlankLines = true;
                using (var reader = new StreamReader(file))
                using (var csv = new CsvHelper.CsvReader(reader, csvConfig))
                {
                    records = new List<StockPrice>(csv.GetRecords<StockPrice>());
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"Error reading {file}: {ex.Message}", ex);
            }
            return records;
        }
    }
}
