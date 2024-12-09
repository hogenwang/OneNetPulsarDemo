using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneNetPulsarDemo.PulsarSubscription
{
    /// <summary>
    /// Pulsar 配置
    /// 根据实际情况填写
    /// </summary>
    public class PulsarConfig
    {
        /// <summary>
        /// 网关 固定地址
        /// </summary>
        public static string Gatewayurl => "pulsar+ssl://iot-north-mq.heclouds.com:6651";
        /// <summary>
        /// 消费组ID
        /// </summary>
        public static string IotAccessId => "";
        /// <summary>
        /// 消费组KEY
        /// </summary>
        public static string IotSecretKey => "";

        /// <summary>
        /// 订阅名称  xxx-sub
        /// </summary>
        public static string IotSubscriptionName => "";
    }
}
