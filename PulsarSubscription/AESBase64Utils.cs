using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OneNetPulsarDemo.PulsarSubscription
{
    public class AESBase64Utils
    {
        private byte[] keyValue;

        // 设置密钥
        public void SetKeyValue(byte[] keyValue)
        {
            this.keyValue = keyValue;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptedData"></param>
        /// <returns></returns>
        public string Decrypt(string encryptedData)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyValue;
                aesAlg.Mode = CipherMode.ECB;  // 使用 ECB 模式
                aesAlg.Padding = PaddingMode.PKCS7;  // 使用 PKCS7 填充模式

                // 解码 base64 编码的数据
                byte[] encryptedBytes = Convert.FromBase64String(encryptedData);

                // 解密数据
                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor())
                {
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static string Decrypt(string data, string secretKey)
        {
            // 创建加解密类
            AESBase64Utils aes = new AESBase64Utils();
            // 设置密钥
            aes.SetKeyValue(Encoding.UTF8.GetBytes(secretKey));
            // 解密并返回
            return aes.Decrypt(data);
        }
    }
}
