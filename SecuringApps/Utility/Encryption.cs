using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecuringApps.Utility
{
    public class Encryption
    {
        static byte[] salt = new byte[]
        {
            20,4,34,56,250,1,65,105,2//,180,101,9,10,200, 5, 7
        };

        static string password = "P@S$W0RD";

        public static SymmetricKeys GenerateKeys()
        {
            Rijndael myAlg = Rijndael.Create();

            Rfc2898DeriveBytes myGenerator = new Rfc2898DeriveBytes(password, salt);

            SymmetricKeys keys = new SymmetricKeys()
            {
                SecretKey = myGenerator.GetBytes(myAlg.KeySize / 8),
                Iv = myGenerator.GetBytes(myAlg.BlockSize / 8)
            };
            return keys;
        }

        //takes clear data and returns the encrypted data
        public static byte[] SymmetricEncrypt(byte[] clearData)
        {
            Rijndael myAlg = Rijndael.Create();

            //1. Generate secret key and iv
            var keys = GenerateKeys();

            //2. Load data into MemoryStream
            MemoryStream msIn = new MemoryStream(clearData);
            msIn.Position = 0;

            //3. declare where to store encrypted data
            MemoryStream msOut = new MemoryStream();

            //4. declaring a stram which handles data encryption
            CryptoStream cs = new CryptoStream(msOut, myAlg.CreateEncryptor(keys.SecretKey, keys.Iv), CryptoStreamMode.Write);

            //5. Start encrypting engine
            msIn.CopyTo(cs);

            //6. Data is flushed into msOut
            cs.FlushFinalBlock();
            cs.Close();

            return msOut.ToArray();
        }

        //clearbytes = original data by user
        //cipher = encrypted data
        public static byte[] SymmetricDecrypt(byte[] cipherAsBytes)
        {
            Rijndael myAlg = Rijndael.Create();

            //1. Generate secret key and iv
            var keys = GenerateKeys();

            //2. Load data into MemoryStream
            MemoryStream msIn = new MemoryStream(cipherAsBytes);
            msIn.Position = 0;

            //3. declare where to store clear data
            MemoryStream msOut = new MemoryStream();

            //4. declaring a stram which handles data decryption
            CryptoStream cs = new CryptoStream(msOut, myAlg.CreateDecryptor(keys.SecretKey, keys.Iv), CryptoStreamMode.Write);

            //5. Start encrypting engine
            msIn.CopyTo(cs);

            //6. Data is flushed into msOut
            cs.FlushFinalBlock();
            cs.Close();

            return msOut.ToArray();
        }

        public static string SymmetricEncrypt(string clearData)
        {
            //1. Convert input by user
            byte[] clearDataAsBytes = Encoding.UTF8.GetBytes(clearData);

            //2. encrypt
            byte[] cipherAsBytes = SymmetricEncrypt(clearDataAsBytes);

            //3. convert to string
            string cipher = Convert.ToBase64String(cipherAsBytes);

            cipher = cipher.Replace('%','P');
            cipher = cipher.Replace('/', 'S');
            cipher = cipher.Replace('+', 'X');
            cipher = cipher.Replace('-', 'M');
            cipher = cipher.Replace('=', 'E');

            return cipher;
        }

        public static string SymmetricDecrypt(string cipher)
        {
            cipher = cipher.Replace('P', '%');
            cipher = cipher.Replace('S', '/');
            cipher = cipher.Replace('X', '+');
            cipher = cipher.Replace('M', '-');
            cipher = cipher.Replace('E', '=');

            //1. Convert input by user
            byte[] cipherDataAsBytes = Convert.FromBase64String(cipher);

            //2. decrypt
            byte[] clearDataAsBytes = SymmetricDecrypt(cipherDataAsBytes);

            //3. convert to string
            string originalText = Encoding.UTF8.GetString(clearDataAsBytes);

            return originalText;
        }
    }

    public class SymmetricKeys
    {
        public byte[] SecretKey { get; set; }
        public byte[] Iv { get; set; }
    }
}
