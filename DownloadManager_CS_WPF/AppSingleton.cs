using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading;
using System.Net.Http;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Data;

using DownloadManager_CS_WPF.DownloadClasses;
using DownloadManager_CS_WPF.FTPConnectionClasses;
using DownloadManager_CS_WPF.ScheduledDownloadClasses;
using DownloadManager_CS_WPF.DownloadClasses.DownloadReportsClasses;

using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace DownloadManager_CS_WPF
{
    public sealed class AppSingleton
    {
        private static AppSingleton instance = null;
        private readonly ObservableCollection<DownloadAbstract> _downloadList;
        private readonly ObservableCollection<FTPConnectionClass> _ftpConnectionsList;
        private readonly ObservableCollection<IScheduledDownload> _scheduledDownloads;
        private readonly Dictionary<int, IDownloadReport> _downloadReports;
        private readonly Random _randomNumberGenerator;

        public ObservableCollection<DownloadAbstract> DownloadsList => _downloadList;

        public ObservableCollection<FTPConnectionClass> FTPConnectionsList => _ftpConnectionsList;

        public ObservableCollection<IScheduledDownload> ScheduledDownloadsList => _scheduledDownloads;

        public Random RandomGenerator => _randomNumberGenerator;

        public Dictionary<int, IDownloadReport> DownloadReport => _downloadReports;

#nullable enable
        public SynchronizationContext? SynchroCotext => SynchronizationContext.Current;
#nullable disable
        AppSingleton()
        {
            _downloadList = new ObservableCollection<DownloadAbstract>();
            _ftpConnectionsList = new ObservableCollection<FTPConnectionClass>();
            _scheduledDownloads = new ObservableCollection<IScheduledDownload>();
            _downloadReports = new Dictionary<int, IDownloadReport>();
            _randomNumberGenerator = new Random();
        }

        public static AppSingleton Instance
        {
            get
            {
                if (instance is null) instance = new AppSingleton();
                return instance;
            }
        }

        public void AddNewDownloadReport(int id,IDownloadReport report)
        {
            DownloadReport.Add(id, report);
        }
    }
}
