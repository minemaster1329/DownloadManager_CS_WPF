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
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            foreach (DownloadAbstract downloadAbstract in AppSingleton.Instance.DownloadsList.Where(x => x.State != DownloadState.DownloadCompleted && x.State != DownloadState.DownloadError && x.State != DownloadState.DownloadCancelled)) downloadAbstract.CancelRetryDownload();
        }
    }
}
