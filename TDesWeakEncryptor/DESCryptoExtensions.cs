using System.Reflection;
using System.Security.Cryptography;

namespace TDesWeakEncryptor
{
    internal static class DESCryptoExtensions
    {
        public static ICryptoTransform CreateWeakEncryptor(this TripleDESCryptoServiceProvider cryptoProvider, byte[] key, byte[] iv)
        {
            MethodInfo mi = cryptoProvider.GetType().GetMethod("_NewEncryptor", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] Par = { key, cryptoProvider.Mode, iv, cryptoProvider.FeedbackSize, 0 };
            ICryptoTransform trans = mi.Invoke(cryptoProvider, Par) as ICryptoTransform;
            return trans;
        }

        public static ICryptoTransform CreateWeakEncryptor(this TripleDESCryptoServiceProvider cryptoProvider)
        {
            return CreateWeakEncryptor(cryptoProvider, cryptoProvider.Key, cryptoProvider.IV);
        }

        public static ICryptoTransform CreateWeakDecryptor(this TripleDESCryptoServiceProvider cryptoProvider, byte[] key, byte[] iv)
        {
            MethodInfo mi = cryptoProvider.GetType().GetMethod("_NewEncryptor", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] Par = { key, cryptoProvider.Mode, iv, cryptoProvider.FeedbackSize, 1 };
            ICryptoTransform trans = mi.Invoke(cryptoProvider, Par) as ICryptoTransform;
            return trans;
        }

        public static ICryptoTransform CreateWeakDecryptor(this TripleDESCryptoServiceProvider cryptoProvider)
        {
            return CreateWeakDecryptor(cryptoProvider, cryptoProvider.Key, cryptoProvider.IV);
        }
    }
}
