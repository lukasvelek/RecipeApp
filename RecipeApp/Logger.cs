using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RecipeApp
{
    public class Logger
    {
        private string filePath;

        private List<string> log;

        private DateTime today;

        public Logger()
        {
            log = new List<string>();

            today = DateTime.Now;

            if (!Directory.Exists("logs"))
            {
                Directory.CreateDirectory("logs");
            }

            filePath = "logs/" + today.Month.ToString() + "-" + today.Day.ToString() + "-" + today.Year.ToString() + ".log";
        }

        public void Log(LogMessageType type, string text)
        {
            log.Add("[" + type.ToString() + "] " + text + " [" + today.ToString() + "]");
        }

        public void LogInfo(string text)
        {
            Log(LogMessageType.INFO, text);
        }

        public void SaveLog()
        {
            string[] data = log.ToArray();

            File.AppendAllLines(filePath, data);
        }
    }
}
