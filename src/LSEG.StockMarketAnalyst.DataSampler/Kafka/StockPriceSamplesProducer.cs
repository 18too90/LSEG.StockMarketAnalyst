using Confluent.Kafka;
using LSEG.StockMarketAnalyst.Domain.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LSEG.StockMarketAnalyst.DataSampler.Kafka
{
    public class StockPriceSamplesProducer : IStockPriceSamplesProducer
    {
        private ClientConfig Config { get; }
        private ILogger<StockPriceSamplesProducer> Logger { get; }

        public StockPriceSamplesProducer(IConfiguration appConfig, ILogger<StockPriceSamplesProducer> logger)
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
        }

        public void Produce(string topicName, string key, List<StockPrice> data)
        {
            // Declare the producer reference here to enable calling the Flush
            // method in the finally block, when the app shuts down.
            IProducer<string, string>? producer = null;

            try
            {
                // Build a producer based on the provided configuration.
                // It will be disposed in the finally block.
                producer = new ProducerBuilder<string, string>(Config).Build();

                // For higher throughput, use the non-blocking Produce call
                // and handle delivery reports out-of-band, instead of awaiting
                // the result of a ProduceAsync call.
                var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                producer.Produce(topicName, new Message<string, string> { Key = key, Value = jsonData },
                    (deliveryReport) =>
                    {
                        if (deliveryReport.Error.Code != ErrorCode.NoError)
                        {
                            Logger.LogWarning($"Failed to deliver message: {deliveryReport.Error.Reason}");
                        }
                        else
                        {
                            Logger.LogWarning($"Produced message to: {deliveryReport.TopicPartitionOffset}");
                        }
                    });


            }
            finally
            {
                var queueSize = producer?.Flush(TimeSpan.FromSeconds(5));
                if (queueSize > 0)
                {
                    Logger.LogWarning("WARNING: Producer event queue has " + queueSize + " pending events on exit.");
                }
                producer?.Dispose();
            }
        }
    }
}
