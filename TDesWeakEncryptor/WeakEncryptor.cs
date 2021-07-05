using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TDesWeakEncryptor
{
    /// <summary>
    /// 
    /// </summary>
    public class WeakEncryptor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] TDes(byte[] data, byte[] key)
        {
            if (key.Length >= 16)
                key = key.SubArray(0, 16);
            else throw new ArgumentException("Wrong key length");
            var iv = new byte[8];
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.Zeros;
            var cTransform = tdes.CreateWeakEncryptor(key, iv);
            var result = cTransform.TransformFinalBlock(data, 0, data.Length);
            tdes.Clear();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static byte[] Encryption(string key, string source)
        {
            var desKey = Encoding.ASCII.GetBytes(key);
            var data = Encoding.ASCII.GetBytes(source);

            if (desKey.Length > 24)
                desKey = desKey.SubArray(0, 24);

            if (desKey.Length != 16 && desKey.Length != 24)
                throw new ArgumentException("Wrong key length");

            var TdesAlg = new TripleDESCryptoServiceProvider();
            TdesAlg.Mode = CipherMode.ECB;
            TdesAlg.Padding = PaddingMode.Zeros;
            TdesAlg.Key = desKey;
            TdesAlg.IV = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            var ict = TdesAlg.CreateEncryptor(TdesAlg.Key, TdesAlg.IV);
            var mStream = new MemoryStream();
            var cStream = new CryptoStream(mStream, ict, CryptoStreamMode.Write);
            cStream.Write(data, 0, data.Length);
            cStream.FlushFinalBlock();
            cStream.Close();
            return mStream.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="desKey"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Decryption(byte[] desKey, byte[] data)
        {
            if (desKey.Length != 16 && desKey.Length != 24)
                throw new ArgumentException("Wrong key length");

            SymmetricAlgorithm TdesAlg = new TripleDESCryptoServiceProvider();
            TdesAlg.Mode = CipherMode.ECB;
            TdesAlg.Padding = PaddingMode.Zeros;

            TdesAlg.Key = desKey;

            TdesAlg.IV = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            ICryptoTransform ict = TdesAlg.CreateDecryptor(TdesAlg.Key, TdesAlg.IV);
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, ict, CryptoStreamMode.Write);
            cStream.Write(data, 0, data.Length);
            cStream.FlushFinalBlock();
            cStream.Close();
            return mStream.ToArray();
        }
    }
}