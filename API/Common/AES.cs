using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace CMS_Core.Common
{
    public class AES
    {
       
        static readonly byte[] u8_Salt = new byte[] { 0x26, 0x19, 0x81, 0x4E, 0xA0, 0x6D, 0x95, 0x34, 0x26, 0x75, 0x64, 0x05, 0xF6 };
        static string keyPrivate = "MedCOm20190401";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncryptString(string plainText, string password)
        {
            try
            {
                password = "M#dl4t4c081188";
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, u8_Salt);
                using (RijndaelManaged i_Alg = new RijndaelManaged { Key = pdb.GetBytes(32), IV = pdb.GetBytes(16) })
                {
                    using (var memoryStream = new MemoryStream())
                    using (var cryptoStream = new CryptoStream(memoryStream, i_Alg.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] data = Encoding.UTF8.GetBytes(plainText);
                        cryptoStream.Write(data, 0, data.Length);
                        cryptoStream.FlushFinalBlock();

                        return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                
                return plainText;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText, string password)
        {
            try
            {
                password = "M#dl4t4c081188";
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, u8_Salt);

                using (RijndaelManaged i_Alg = new RijndaelManaged { Padding = PaddingMode.Zeros, Key = pdb.GetBytes(32), IV = pdb.GetBytes(16) })
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, i_Alg.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            byte[] data = Convert.FromBase64String(cipherText);
                            cryptoStream.Write(data, 0, data.Length);
                            cryptoStream.Flush();

                            return RegExReplace(Encoding.UTF8.GetString(memoryStream.ToArray()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                return cipherText;
            }
        }

        public static String CreateKey(int numBytes)
        {
            try
            {
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] buff = new byte[numBytes];

                rng.GetBytes(buff);
                return keyPrivate + BytesToHexString(buff);
            }
            catch (Exception ex)
            {
                
                return keyPrivate;
            }
        }


        static String BytesToHexString(byte[] bytes)
        {
            StringBuilder hexString = new StringBuilder(64);

            for (int counter = 0; counter < bytes.Length; counter++)
            {
                hexString.Append(String.Format("{0:X2}", bytes[counter]));
            }
            return hexString.ToString();
        }


        private static String RegExReplace(string inputString)
        {

            if (!string.IsNullOrEmpty(inputString))
            {
                inputString = inputString.Replace("\u0002", string.Empty);
                inputString = inputString.Replace("\u0003", string.Empty);
                inputString = inputString.Replace("\u0001", string.Empty);
                inputString = inputString.Replace("\u0006", string.Empty);
                inputString = inputString.Replace("\u0005", string.Empty);
                inputString = inputString.Replace("\u0004", string.Empty);
                inputString = inputString.Replace("\u0010", string.Empty);
                inputString = inputString.Replace("\0", string.Empty);
                inputString = inputString.Replace("\f", string.Empty);
                inputString = inputString.Replace("\a", string.Empty);
                inputString = inputString.Replace("\b", string.Empty);

                inputString = inputString.Replace("\n", string.Empty);
                inputString = inputString.Replace("\r", string.Empty);
                inputString = inputString.Replace("\t", string.Empty);
                inputString = inputString.Replace("\v", string.Empty);

             

                inputString = Regex.Replace(inputString, @"(\\u000)+$", string.Empty);
                inputString = Regex.Replace(inputString, @"[^\u0000-\u007F]", "");
            }           
            return inputString;

        }


    }
}
