using DotPulsar;
using DotPulsar.Extensions;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OneNetPulsarDemo.PulsarSubscription
{
    /// <summary>
    /// OneNet开放Pulsar订阅 消费
    /// </summary>
    public class IoTPulsarConsume
    {
        public static async ValueTask DoStartConsume()
        {
            string topic = $"{PulsarConfig.IotAccessId}/iot/event";
            var client = PulsarClient.Builder()
            .ServiceUrl(new Uri(PulsarConfig.Gatewayurl))
            .ConnectionSecurity(EncryptionPolicy.EnforceEncrypted)
            .VerifyCertificateName(true)
            .VerifyCertificateAuthority(false)
            .Authentication(new IotAuthentication(PulsarConfig.IotAccessId, PulsarConfig.IotSecretKey))
            .RetryInterval(TimeSpan.FromSeconds(3))
            .Build();

            var consumer = client.NewConsumer(Schema.String)
                     .StateChangedHandler(PMonitor)
                     .Topic(topic)
                     .SubscriptionName(PulsarConfig.IotSubscriptionName)
                     .SubscriptionType(SubscriptionType.Failover)
                     .Create();

            try
            {
                await foreach (var message in consumer.Messages())
                {
                    string data = Encoding.UTF8.GetString(message.Data.ToArray());
                    var obj = JsonSerializer.Deserialize<PulsarMessageBody>(data);
                    // 解密  
                    string decryptedMessage = AESBase64Utils.Decrypt(obj.data, PulsarConfig.IotSecretKey.Substring(8, 16));
                    Console.WriteLine($"订阅OneNet 消息 {DateTime.Now}:{decryptedMessage}");
                    //真正处理数据业务逻辑，请自己实现


                    await consumer.AcknowledgeCumulative(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static void PMonitor(ConsumerStateChanged stateChanged, CancellationToken cancellationToken)
        {
            var stateMessage = stateChanged.ConsumerState switch
            {
                ConsumerState.Active => "is active",
                ConsumerState.Inactive => "is inactive",
                ConsumerState.Disconnected => "is disconnected",
                ConsumerState.Closed => "has closed",
                ConsumerState.ReachedEndOfTopic => "has reached end of topic",
                ConsumerState.Faulted => "has faulted",
                _ => $"has an unknown state '{stateChanged.ConsumerState}'"
            };
            var topic = stateChanged.Consumer.Topic;
            Console.WriteLine($"The consumer for topic '{topic}' " + stateMessage);
        }
    }


}
