using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.IO;

using DownloadManager_CS_WPF.DownloadClasses;

namespace DownloadManager_CS_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (!Directory.Exists("Logs"))
            {
                Directory.CreateDirectory("Logs");
            }

            DateTime dateTime = DateTime.Now;

            string file_name = $"Logs/{dateTime.Hour}_{dateTime.Minute}_{dateTime.Second}__{dateTime.Day}_{dateTime.Month}_{dateTime.Year}.log";

            FileStream fs =  File.Create(file_name);
            fs.Close();

            if (!File.Exists(@"Logs/logs.json"))
            {
                FileStream fs2 = File.Create(@"Logs/logs.json");
                fs2.Close();
            }

            AppSingleton.Instance.LogsFileName = file_name;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            foreach (DownloadAbstract downloadAbstract in AppSingleton.Instance.DownloadsList.Where(x => x.State != DownloadState.DownloadCompleted && x.State != DownloadState.DownloadError && x.State != DownloadState.DownloadCancelled)) downloadAbstract.CancelRetryDownload();
        }
    }
}
