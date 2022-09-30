using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace ContosoUniversityAPI.HelperClasses
{
    public class Globals
    {
        public static string conn = ConfigurationManager.ConnectionStrings["ConnectionAPI"].ConnectionString;
        //Error types
        public const string SUCCESS = "000";
        //public const string APPLICATION_ERROR = "001";
        public const string DATABASE_READING_ERROR = "002";
        public const string DATABASE_WRITING_ERROR = "003";
        public const string DATABASE_UPDATE_ERROR = "004";
        //public const string UNDEFINED = "005";
        public const string GENERIC_CATCH_ERROR = "006";
        public const string TOKEN_MISMATCH_OR_MISSING = "007";
        public const string NO_RESULTS = "008";
        public const string FAILED_TO_SAVE_FILE = "010";
        //login related
        public const string PASSWORD_MISMATCH = "009";
        public const string INACTIVE_USER = "012";
        public const string ACCOUNT_EXPIRED = "013";
        public const string USER_DOESNT_EXIST = "014";
        public const string WRONG_OLD_PASSWORD = "015";
        public const string NEW_PASSWORD_MISMATCH = "016";
        public const string INVALID_PASSWORD_POLICY = "017";
        // carrier related
        public const string CARRIER_NOT_FOUND = "018";
        public const string BOXES_NOT_FOUND = "019";
        public const string CARRIER_SERVICE_ERROR = "020";

        public static String CreateJSON(object item)
        {
            return JsonConvert.SerializeObject(item);
        }
        public static string HashPassword(string theInput)
        {
            using (MD5 hasher = MD5.Create())
            {
                byte[] dbytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(theInput));
                StringBuilder sBuilder = new StringBuilder();
                for (int n = 0; n <= dbytes.Length - 1; n++)
                {
                    sBuilder.Append(dbytes[n].ToString("X2"));
                }

                return sBuilder.ToString();
            }
        }
        public static bool CheckToken(string pass, string checkpass)
        {
            //tries to compare the password introduced with the one in the database. returns true if they are the same and false if they are not.
            byte[] hashbytes = Convert.FromBase64String(pass);
            byte[] salt = new byte[64];
            Array.Copy(hashbytes, 0, salt, 0, 64);
            var hashing = new Rfc2898DeriveBytes(checkpass, salt, 10000);
            byte[] hash = hashing.GetBytes(64);
            for (int i = 0; i < 64; i++)
            {
                if (hashbytes[i + 64] != hash[i])
                    return false;
            }
            return true;
        }

        public static string HashToken(string password)
        {
            byte[] salt;
            byte[] hash;
            byte[] hashbytes = new byte[128];
            string hashedpassword = "";

            new RNGCryptoServiceProvider().GetBytes(salt = new byte[64]);
            var hashing = new Rfc2898DeriveBytes(password, salt, 10000);
            hash = hashing.GetBytes(64);
            Array.Copy(salt, 0, hashbytes, 0, 64);
            Array.Copy(hash, 0, hashbytes, 64, 64);

            hashedpassword = Convert.ToBase64String(hashbytes);

            return hashedpassword;
        }


        private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            var saltBytes = new byte[] { 0, 6, 0, 7, 1, 9, 9, 1 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 10000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }

                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }

        public static string EncryptToken(string plainText, string password)
        {
            if (plainText == null)
            {
                return null;
            }

            if (password == null)
            {
                password = String.Empty;
            }

            // Get the bytes of the string
            var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            var bytesEncrypted = Encrypt(bytesToBeEncrypted, passwordBytes);

            return Convert.ToBase64String(bytesEncrypted);
        }


        public static string DecryptToken(string encryptedText, string password)
        {
            //checks if the passed text is null
            if (encryptedText == null)
            {
                return null;
            }
            //checks if the password is null
            if (password == null)
            {
                password = String.Empty;
            }

            // Get the bytes of the string and tries to decrypt it using the passed information
            var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            var bytesDecrypted = Decrypt(bytesToBeDecrypted, passwordBytes);

            return Encoding.UTF8.GetString(bytesDecrypted);
        }

        private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            var saltBytes = new byte[] { 0, 6, 0, 7, 1, 9, 9, 1 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 10000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }

                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        public static string ReadHtmlTemplate(string file)
        {
            //string tPath = System.Reflection.Assembly.GetExecutingAssembly().Location + @"\Templates\";
            string tPath = ConfigurationManager.AppSettings["TemplatePath"] + @"\";
            //tPath = tPath.Replace(@"\bin\Release", "");
            //tPath = tPath.Replace(@"\bin\Debug", "");
            //tPath = tPath.Replace(@"\LivithSmartAPI.dll", "");
            System.IO.StreamReader Output = new StreamReader(tPath + file, false);
            string result = Output.ReadToEnd();
            Output.Close();

            return result;
        }

        public static void SendEmail(string toAddress, string bccAddress, string mailSubject, string mailBody, string fileAttach)
        {
            string fromEmail = ConfigurationManager.AppSettings["Email"];
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
            string host = ConfigurationManager.AppSettings["SMTPHost"];
            string userName = ConfigurationManager.AppSettings["Email"];
            string password = ConfigurationManager.AppSettings["Password"];

            MailMessage mail = new MailMessage(fromEmail, toAddress);

            if (bccAddress != "")
            {
                mail.Bcc.Add(bccAddress);
            }

            SmtpClient client = new SmtpClient();
            client.Port = port;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = host;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(userName, password);
            mail.IsBodyHtml = true; //to make message body as html 
            mail.Subject = mailSubject;
            if (fileAttach != "")
            {
                mail.Attachments.Add(new Attachment(fileAttach));
            }

            mail.Body += mailBody;

            client.Send(mail);
        }
    }
}