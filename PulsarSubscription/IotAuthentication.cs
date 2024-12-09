using DotPulsar.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OneNetPulsarDemo.PulsarSubscription
{
    public class IotAuthentication : IAuthentication
    {
        private string iotAccessId;
        private string iotSecretKey;

        public IotAuthentication(string iotAccessId, string iotSecretKey)
        {
            this.iotAccessId = iotAccessId;
            this.iotSecretKey = iotSecretKey;
        }

        public string AuthenticationMethodName => "iot-auth";

        public ValueTask<byte[]> GetAuthenticationData(CancellationToken cancellationToken)
        {
            string password = SHA256Hex(iotAccessId + SHA256Hex(iotSecretKey)).Substring(4, 16);
            string TokenStr = $"{{\"tenant\":\"{iotAccessId}\",\"password\":\"{password}\"}}";
            var result_bytes = Encoding.UTF8.GetBytes(TokenStr);
            return ValueTask.FromResult(result_bytes);
        }

        #region SHA256Hex
        public static string SHA256Hex(string input)
        {
            using (SHA256 sha256Hash = System.Security.Cryptography.SHA256.Create())
            {
                // 将输入字符串转换为字节数组并计算哈希值
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // 将字节数组转换为十六进制字符串
                StringBuilder builder = new StringBuilder();
                foreach (byte byteValue in bytes)
                {
                    builder.Append(byteValue.ToString("x2")); // x2 表示将字节格式化为2位的十六进制数
                }
                return builder.ToString();
            }
        }
        #endregion

    }


}
