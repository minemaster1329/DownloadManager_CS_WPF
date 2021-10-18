using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

namespace DownloadManager_CS_WPF
{
    public enum LogType { Info, Error, Warning, Other}
    public sealed class CustomLoggerSingleton
    {
        
        private static CustomLoggerSingleton _instance = new CustomLoggerSingleton();
        private ObservableCollection<string> _logs = new ObservableCollection<string>();
        private CultureInfo _cultureInfo = new CultureInfo("PL-pl");
        private Mutex _logsCollectionMutex = new Mutex();
        public static CustomLoggerSingleton Instance => _instance;
        public ObservableCollection<string> Logs => _logs;

        private CustomLoggerSingleton()
        {
            ReadLogsFromFile();
        }

        private void ReadLogsFromFile()
        {
            if (!File.Exists(@"Logs\logs.log"))
            {
                FileStream fs = File.Create(@"Logs\logs.log");
                fs.Close();
            }

            else
            {
                string[] str = File.ReadAllLines(@"Logs\logs.log");
                foreach (string s in str) Logs.Add(s);
            }
        }

        public void AddNewLog(string caption, string message, LogType logType)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append('[');
            stringBuilder.Append(DateTime.Now.ToString(_cultureInfo));
            stringBuilder.Append("] ");
            switch (logType)
            {
                case LogType.Error:
                    stringBuilder.Append("ERROR");
                    break;
                case LogType.Info:
                    stringBuilder.Append("INFO");
                    break;
                case LogType.Warning:
                    stringBuilder.Append("WARNING");
                    break;
                case LogType.Other:
                    stringBuilder.Append("OTHER");
                    break;
            }
            stringBuilder.Append($" {caption} : {message}");
            _logsCollectionMutex.WaitOne();
            Logs.Add(stringBuilder.ToString());
            using StreamWriter streamWriter = new StreamWriter(@"Logs\logs.log", true);
            streamWriter.WriteLine(stringBuilder.ToString());
            _logsCollectionMutex.ReleaseMutex();
        }
    }
}
