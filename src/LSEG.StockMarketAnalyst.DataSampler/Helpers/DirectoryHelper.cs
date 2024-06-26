using System.Collections.Generic;
using System.Configuration;

namespace LSEG.StockMarketAnalyst.DataSampler.Helpers
{
    /// <summary>
    /// Helps to access data directory
    /// </summary>
    public class DirectoryHelper : IDirectoryHelper
    {
        private readonly string dataDir;
        private ILogger<DirectoryHelper> Logger { get; }

        public DirectoryHelper(IConfiguration configuration, 
            ILogger<DirectoryHelper> logger)
        {
            dataDir = configuration.GetValue<string>("DataSetFolder") ?? string.Empty;
            Logger = logger;
        }

        /// <summary>
        /// Get files as from data directory
        /// </summary>
        /// <param name="countPerExchange"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IEnumerable<KeyValuePair<string, IEnumerable<string>>> GetFiles(int countPerExchange = 1)
        {
            if (countPerExchange == 0) throw new ArgumentException("NoOfFiles to be considered should be atleast 1");
            IEnumerable<string>? directories;
            try
            {
                directories = Directory.GetDirectories(dataDir);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                yield break;
            }
            if (directories == null)
            {
                yield break;
            }
            foreach (var directoryPath in directories)
            {
                string? dirName;
                IEnumerable<string>? files;
                try
                {
                    dirName = directoryPath.Split("/").LastOrDefault() ?? string.Empty;

                    files = Directory.GetFiles(directoryPath).Take(countPerExchange);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex.Message, ex);
                    yield break;
                }
                yield return new KeyValuePair<string, IEnumerable<string>>(dirName, files);

            }
        }
    }
}

