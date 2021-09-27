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

using DownloadManager_CS_WPF.CustomDynamicList;
using DownloadManager_CS_WPF.DownloadClasses;
using DownloadManager_CS_WPF.DebugInfoClasses;
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
        private readonly CustomDynamicLinkedList<DebugInfoAbstract> _logs;
        private readonly ObservableCollection<DownloadAbstract> _downloadList;
        private readonly ObservableCollection<FTPConnectionClass> _ftpConnectionsList;
        private readonly ObservableCollection<IScheduledDownload> _scheduledDownloads;
        private readonly Dictionary<int, IDownloadReport> _downloadReports;
        private readonly Random _randomNumberGenerator;
        
        private string _logs_file_name;
        private StreamWriter _logs_file_stream_writer;

        public CustomDynamicLinkedList<DebugInfoAbstract> Logs => _logs;

        public ObservableCollection<DownloadAbstract> DownloadsList => _downloadList;

        public ObservableCollection<FTPConnectionClass> FTPConnectionsList => _ftpConnectionsList;

        public ObservableCollection<IScheduledDownload> ScheduledDownloadsList => _scheduledDownloads;

        public Random RandomGenerator => _randomNumberGenerator;

        public Dictionary<int, IDownloadReport> DownloadReport => _downloadReports;

        public string LogsFileName
        {
            get => _logs_file_name;
            set
            {
                _logs_file_stream_writer?.Close();
                _logs_file_name = value;
                _logs_file_stream_writer = new StreamWriter(value);
            }
        }
#nullable enable
        public SynchronizationContext? SynchroCotext => SynchronizationContext.Current;
#nullable disable
        AppSingleton()
        {
            _logs = new CustomDynamicLinkedList<DebugInfoAbstract>();
            _downloadList = new ObservableCollection<DownloadAbstract>();
            _logs.CollectionChanged += Logs_CollectionChanged;
            _ftpConnectionsList = new ObservableCollection<FTPConnectionClass>();
            _scheduledDownloads = new ObservableCollection<IScheduledDownload>();
            _downloadReports = new Dictionary<int, IDownloadReport>();
            _randomNumberGenerator = new Random();
            StreamReader streamReader = new StreamReader(@"Logs/logs.json");
            string json = streamReader.ReadLine();
            streamReader.Close();
            if (json is null)
            {
                _logs = new CustomDynamicLinkedList<DebugInfoAbstract>();
            }
            else
            {
                _logs = JsonConvert.DeserializeObject<CustomDynamicLinkedList<DebugInfoAbstract>>(json, new DebugInfoAbstractConverter());
            }
            _logs.CollectionChanged += Logs_CollectionChanged;
        }

        private void Logs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (DebugInfoAbstract a in e.NewItems) _logs_file_stream_writer?.WriteLine(a);
                using StreamWriter _all_logs_writer = new(@"Logs/logs.json");
                JsonSerializer jsonSerializer = new();
                jsonSerializer.Serialize(_all_logs_writer, _logs);
            }
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
