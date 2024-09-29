using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography;
using System.Text;

namespace Application.Common.Utility;

public static class EncryptDecryptHelper
{
    public static string EncryptByAes(string plainText, string secret)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException(nameof(plainText));

        if (secret == null || secret.Length <= 0)
            throw new ArgumentNullException(nameof(secret));

        secret = secret.Replace("-", string.Empty);

        var key = Encoding.UTF8.GetBytes(secret);
        var iv = Encoding.UTF8.GetBytes(secret.Substring(0, 16));

        return EncryptByAes(plainText, key, iv);
    }

    public static string EncryptByAes(string plainText, byte[] key, byte[] iv)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException(nameof(plainText));

        if (key == null || key.Length <= 0)
            throw new ArgumentNullException(nameof(key));

        if (iv == null || iv.Length <= 0)
            throw new ArgumentNullException(nameof(iv));

        // Create an Aes object
        // with the specified key and IV.
        using (RijndaelManaged cipher = new RijndaelManaged())
        {
            cipher.Key = key;
            cipher.IV = iv;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = cipher.CreateEncryptor(cipher.Key, cipher.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
    }

    public static string DecryptByAes(string cipherText, string secret)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException(nameof(cipherText));

        if (secret == null || secret.Length <= 0)
            throw new ArgumentNullException(nameof(secret));

        secret = secret.Replace("-", string.Empty);

        if (secret.Length != 32)
            throw new ArgumentException(nameof(secret));

        var key = Encoding.UTF8.GetBytes(secret);
        var iv = Encoding.UTF8.GetBytes(secret.Substring(0, 16));

        cipherText = cipherText.Replace(" ", "+");

        return DecryptByAes(cipherText, key, iv);
    }

    public static string DecryptByAes(string cipherText, byte[] key, byte[] iv)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException(nameof(cipherText));

        if (key == null || key.Length <= 0)
            throw new ArgumentNullException(nameof(key));

        if (iv == null || iv.Length <= 0)
            throw new ArgumentNullException(nameof(iv));

        // Convert the plaintext string to a byte array
        byte[] encryptedBytes = Convert.FromBase64String(cipherText);

        // Create an Aes object
        // with the specified key and IV.
        using (RijndaelManaged cipher = new RijndaelManaged())
        {
            cipher.Key = key;
            cipher.IV = iv;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = cipher.CreateDecryptor(cipher.Key, cipher.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }
}
