using LSEG.StockMarketAnalyst.Domain.Models;
using System.Globalization;

namespace LSEG.StockMarketAnalyst.OutlierDetector.Helpers
{
    /// <summary>
    /// File operations
    /// </summary>
    public class FileHelper : IFileHelper
    {
        private readonly ILogger<FileHelper> _logger;
        private readonly string _directoryPath;

        public FileHelper(ILogger<FileHelper> logger, IConfiguration configuration)
        {
                _logger = logger;
            _directoryPath = configuration["DataSetFolder"]?? string.Empty;
        }

        public void WriteFiles(KeyValuePair<string, List<StockPrice>> outliers)
        {
            var folderName = outliers.Key.Split("_")[0];
            var fileName = outliers.Key.Split("_")[1] + "_Outliers.csv";

            var filePath = Path.Combine(_directoryPath, folderName + '/' + fileName);
            try
            {
                using (var writer = new StreamWriter(filePath))
                using (var csv = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(outliers.Value);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}
