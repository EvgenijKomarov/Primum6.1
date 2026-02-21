using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace AnonimousTokenProducer
{
    public class SignTokenWorker
    {
        private static readonly byte[] Key =
        Encoding.UTF8.GetBytes("12345678901234567890123456789012");

        private static readonly byte[] IV =
            Encoding.UTF8.GetBytes("1234567890123456");


        // ---------- PUBLIC ----------

        public string EncryptSign(ChatBotSign obj)
        {
            string json = JsonSerializer.Serialize(obj);
            byte[] data = Encoding.UTF8.GetBytes(json);

            byte[] encrypted = EncryptBytes(data);

            return Base62Encode(encrypted);
        }

        public ChatBotSign? DecryptSign(string encryptedSign)
        {
            byte[] encryptedBytes = Base62Decode(encryptedSign);

            byte[] decrypted = DecryptBytes(encryptedBytes);

            string json = Encoding.UTF8.GetString(decrypted);

            return JsonSerializer.Deserialize<ChatBotSign>(json);
        }


        // ---------- AES ----------

        private static byte[] EncryptBytes(byte[] data)
        {
            using Aes aes = Aes.Create();

            aes.Key = Key;
            aes.IV = IV;

            using var ms = new MemoryStream();

            using var cs = new CryptoStream(ms,
                aes.CreateEncryptor(),
                CryptoStreamMode.Write);

            cs.Write(data, 0, data.Length);
            cs.FlushFinalBlock();

            return ms.ToArray();
        }


        private static byte[] DecryptBytes(byte[] data)
        {
            using Aes aes = Aes.Create();

            aes.Key = Key;
            aes.IV = IV;

            using var ms = new MemoryStream(data);

            using var cs = new CryptoStream(ms,
                aes.CreateDecryptor(),
                CryptoStreamMode.Read);

            using var result = new MemoryStream();

            cs.CopyTo(result);

            return result.ToArray();
        }


        // ---------- BASE62 ----------

        private const string Alphabet =
            "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";


        private static string Base62Encode(byte[] data)
        {
            var sb = new StringBuilder();

            foreach (byte b in data)
            {
                sb.Append(Alphabet[b / 62]);
                sb.Append(Alphabet[b % 62]);
            }

            return sb.ToString();
        }


        private static byte[] Base62Decode(string text)
        {
            var result = new byte[text.Length / 2];

            for (int i = 0; i < result.Length; i++)
            {
                int high = Alphabet.IndexOf(text[i * 2]);
                int low = Alphabet.IndexOf(text[i * 2 + 1]);

                result[i] = (byte)(high * 62 + low);
            }

            return result;
        }
    }
}
