using Confluent.Kafka;
using LSEG.StockMarketAnalyst.Domain.Models;
using LSEG.StockMarketAnalyst.OutlierDetector.Helpers;
using LSEG.StockMarketAnalyst.OutlierDetector.Managers;

namespace LSEG.StockMarketAnalyst.OutlierDetector.Kafka
{
    public class StocksPriceSamplesConsumer : IStocksPriceSamplesConsumer
    {
        private ClientConfig Config { get; }
        private ILogger<StocksPriceSamplesConsumer> Logger { get; }
        private IOutlierManager OutlierManager { get; }
        private IFileHelper FileHelper { get; }

        public StocksPriceSamplesConsumer(IConfiguration appConfig, ILogger<StocksPriceSamplesConsumer> logger, IOutlierManager outlierManager, IFileHelper fileHelper)
        {
            var clientConfig = new ClientConfig();
            clientConfig.BootstrapServers = "pkc-7prvp.centralindia.azure.confluent.cloud:9092";
            clientConfig.SecurityProtocol = Confluent.Kafka.SecurityProtocol.SaslSsl;
            clientConfig.SaslMechanism = Confluent.Kafka.SaslMechanism.Plain;
            clientConfig.SaslUsername = appConfig["Kafka:Key"];
            clientConfig.SaslPassword = appConfig["Kafka:Secret"];
            clientConfig.SslCaLocation = "probe";
            Config = clientConfig;
            Logger = logger;
            OutlierManager = outlierManager;
            FileHelper = fileHelper;
        }

        public void Consume(string topicName, CancellationToken cancellation = default)
        {
            Console.WriteLine($"{nameof(Consume)} starting");

            // Configure the consumer group based on the provided configuration. 
            var consumerConfig = new ConsumerConfig(Config);
            consumerConfig.GroupId = "stock-price-sample-consumer-group-1";
            // The offset to start reading from if there are no committed offsets (or there was an error in retrieving offsets).
            consumerConfig.AutoOffsetReset = AutoOffsetReset.Earliest;
            // Do not commit offsets.
            consumerConfig.EnableAutoCommit = false;


            // Build a consumer that uses the provided configuration.
            using (var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build())
            {
                // Subscribe to events from the topic.
                consumer.Subscribe(topicName);
                try
                {
                    // Run until the terminal receives Ctrl+C. 
                    while (true)
                    {
                        // Consume and deserialize the next message.
                        var cr = consumer.Consume(cancellation);
                        // Parse the JSON to extract the URI of the edited page.
                        var jsonDoc = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StockPrice>>(cr.Message.Value);
                        // For consuming from the recent_changes topic. 

                        if (jsonDoc != null)
                        {
                            var outliers = OutlierManager.FindOutliers(jsonDoc);

                            if (outliers != null && outliers.Any())
                            {
                                FileHelper.WriteFiles(
                                    new KeyValuePair<string, List<StockPrice>>(cr.Message.Key, outliers.ToList()));
                            }
                        }
                    }

                }
                finally
                {
                    consumer.Close();
                }
            }
        }

    }
}