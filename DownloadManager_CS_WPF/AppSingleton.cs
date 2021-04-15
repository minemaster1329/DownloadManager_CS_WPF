using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading;
using DownloadManager_CS_WPF.CustomDynamicList;
using DownloadManager_CS_WPF.DownloadClasses;

namespace DownloadManager_CS_WPF
{
    public sealed class AppSingleton
    {
        private static AppSingleton instance = null;
        private readonly CustomDynamicLinkedList<IDebugInfo> _logs;
        private ObservableCollection<DownloadAbstract> _downloadList;

        public CustomDynamicLinkedList<IDebugInfo> Logs
        {
            get => _logs;
        }

        public ObservableCollection<DownloadAbstract> DownloadsList
        {
            get => _downloadList;
            set
            {
                _downloadList = value;
            }
        }

        AppSingleton()
        {
            _logs = new CustomDynamicLinkedList<IDebugInfo>();
            _downloadList = new ObservableCollection<DownloadAbstract>();
        }

        public static AppSingleton Instance
        {
            get
            {
                if (instance is null) instance = new AppSingleton();
                return instance;
            }
        }
    }
}
