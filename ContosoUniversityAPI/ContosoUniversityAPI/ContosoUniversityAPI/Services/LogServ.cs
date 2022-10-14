using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ContosoUniversityAPI.Services
{
    public class LogServ
    {
        //creates the folder and file of log if not existent already.
        public static string GetAndCreateLogPath()
        {
            string tPath = ConfigurationManager.AppSettings["LogsRepository"];
            System.IO.DirectoryInfo Folder = new System.IO.DirectoryInfo(tPath);
            if (Folder.Exists == false)
            {
                Folder.Create();
            }

            return tPath + DateTime.Now.ToString("yyyMMdd") + "_" + Assembly.GetCallingAssembly().GetName().Name + "_log.txt";
        }

        //Used when an error occures. It is complete to make sure it can be easily identified and corrected.
        public static void WriteError(string Title, string Description, Exception ex, string extra = "No Extra Data")
        {
            string error = string.Empty;
            error = "Time of Error: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + System.Environment.NewLine;
            error += Title + System.Environment.NewLine;
            error += Description + System.Environment.NewLine;
            error += "Message: " + ex.Message + System.Environment.NewLine;
            error += "HelpLink: " + ex.HelpLink + System.Environment.NewLine;
            error += "StackTrace: " + ex.StackTrace + System.Environment.NewLine;
            error += "Sql/Json: " + extra + System.Environment.NewLine;
            WriteLog(error);
        }

        //Used when LogsVerbose is set to true. This will log every action the program does.
        public static void WriteInfo(string Title, string Description, string extra = "No Extra Data")
        {
            if (ConfigurationManager.AppSettings["LogsVerbose"] == "true")
            {
                string info = string.Empty;
                info = "Time of Info: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + System.Environment.NewLine;
                info += Title + System.Environment.NewLine;
                info += Description + System.Environment.NewLine;
                info += "Sql/Json: " + extra + System.Environment.NewLine;
                WriteLog(info);
            }
        }

        //Mostly for debug purposes. It is used mostly only where needed to check stuff up or to make sure it is not confused with others
        public static void WriteDebug(string Title, string Description, string extra = "No Extra Data")
        {
            string debug = string.Empty;
            debug = "Time of Debug: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + System.Environment.NewLine;
            debug += Title + System.Environment.NewLine;
            debug += Description + System.Environment.NewLine;
            debug += "Sql/Json: " + extra + System.Environment.NewLine;
            WriteLog(debug);
        }

        //Takes care of writing into the log file whatever message passed to it into the file. Also write down the time of creation
        private static void WriteLog(string line)
        {
            try
            {
                System.IO.StreamWriter Output = new System.IO.StreamWriter(GetAndCreateLogPath(), true);
                if (!File.Exists(GetAndCreateLogPath()))
                {
                    Output.Write("Creation: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + System.Environment.NewLine + System.Environment.NewLine);
                }

                Output.Write(line);
                Output.Close();
            }
            catch
            {
                return;
            }
        }
    }
}