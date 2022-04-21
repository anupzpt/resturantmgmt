using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DMS.Helpers
{
    public static class DSHelper
    {
        public static string Prefix => "pkcs7";
        public static string format => "xml";
        public static string commonName => "CommonName";

        public static string stripSignature(string FullSign)
        {
            int posCommonName = FullSign.IndexOf(commonName);
            int iLen = "Signature=".Length;
            string rep = System.Text.RegularExpressions.Regex.Replace(FullSign.Substring(iLen, posCommonName - iLen).Trim(),
                @"\n|\r/g",
                "");
            return rep;
        }

        public static HashWithSaltResult HashData(string password, int saltLength, HashAlgorithm hashAlgo)
        {
            RNG rng = new RNG();
            byte[] saltBytes = Encoding.UTF8.GetBytes(Properties.Settings.Default.PwdSalt);
            // rng.GenerateRandomCryptographicBytes(saltLength);
            byte[] passwordAsBytes = Encoding.UTF8.GetBytes(password);
            List<byte> passwordWithSaltBytes = new List<byte>();
            passwordWithSaltBytes.AddRange(passwordAsBytes);
            passwordWithSaltBytes.AddRange(saltBytes);
            byte[] digestBytes = hashAlgo.ComputeHash(passwordWithSaltBytes.ToArray());
            return new HashWithSaltResult(Convert.ToBase64String(saltBytes), Convert.ToBase64String(digestBytes));

        }
        public static string HashString(string password)
        {
            return HashData(password, 64, SHA512.Create()).Digest;
        }
        public static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        public static bool SavePDFSignFile(string SignedPdf, string FileName, string UserName, bool ValidateSignEmail = false)
        {
            if (SignedPdf == null) { throw new Exception("Signed PDF String is null."); }
            if (SignedPdf.Trim().Length <= 0) { throw new Exception("Signed PDF String cannot be spaces or length less than 0."); }
            int preFix = "signature= ".Length;
            var a = SignedPdf.Substring(preFix);
            var b = a.IndexOf("Common name");


            var x = a.Substring(b).Length;


            var c = a.Substring(0, a.Length - x);
            if (c.Trim().Length <= 0) { throw new Exception("Signature PDF String withtout extra data cannot be space or less than 0."); }

            byte[] bytes = Convert.FromBase64String(c);

            var rem = SignedPdf.Substring(b + preFix, SignedPdf.Length - b - preFix);
            var lines = rem.Split('\n');
            Dictionary<string, string> dKey = new Dictionary<string, string>();
            foreach (var item in lines)
            {
                var lkey = item.Split(new string[] { "= " }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < (lkey.Count() / 2); i++)
                {
                    dKey.Add(lkey[i].Trim(), lkey[i + 1].Trim());
                }
            }

            //now for subject dn
            var lineSubject = dKey["Subject DN"];
            var sLines = lineSubject.Split(',');
            foreach (var item in sLines)
            {
                var lkey = item.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < (lkey.Count() / 2); i++)
                {
                    dKey.Add(lkey[i].Trim(), lkey[i + 1].Trim());
                }
            }
            //validate email id now
            if (ValidateSignEmail)
            {
                if ((dKey["EMAILADDRESS"]).ToUpper() != (UserName).ToUpper())
                {
                    throw new Exception("Document Signer Email ID and Login ID Email id don't match.");
                }
            }

            string filePath = FileName;
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            FileStream stream = new FileStream(filePath, FileMode.CreateNew);
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(bytes, 0, bytes.Length);
            writer.Close();

            return true;
        }
    }
    public class RNG
    {
        public string GenerateRandomCryptographicKey(int keyLength)
        {
            return Convert.ToBase64String(GenerateRandomCryptographicBytes(keyLength));
        }

        public byte[] GenerateRandomCryptographicBytes(int keyLength)
        {
            RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[keyLength];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return randomBytes;
        }
    }
    public class HashWithSaltResult
    {
        public string Salt { get; }
        public string Digest { get; set; }

        public HashWithSaltResult(string salt, string digest)
        {
            Salt = salt;
            Digest = digest;
        }
    }
}
