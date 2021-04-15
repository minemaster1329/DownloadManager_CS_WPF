using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DownloadManager_CS_WPF;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Diagnostics;
using DownloadManager_CS_WPF.CustomDynamicList;
using System.Threading.Tasks;
using System.Threading;
using DownloadManager_CS_WPF.DownloadClasses;
using DownloadManager_CS_WPF.Windows;

namespace DownloadManager_CS_WPF.ViewModels
{
    public class MainWindowViewModel : PropertyChangedHandlerClass
    {

        #region RelayCommandsDefinitions
        #region Sync
        RelaySyncCommand _downloadFromFTPButtonCommand;
        RelaySyncCommand _downloadFromWebButtonCommand;
        RelaySyncCommand _pauseDownloadRelayButtonCommand;
        RelaySyncCommand _cancelDownloadRelayButtonCommand;
        RelaySyncCommand _clearDownloadListButtonCommand;
        RelaySyncCommand _clearLogsListButtonCommand;
        #endregion
        #region Async
        #endregion
        #endregion

        public MainWindowViewModel()
        {
            _downloadFromFTPButtonCommand = new RelaySyncCommand(DownloadFromFTPButtonCommandExecute);
            _downloadFromWebButtonCommand = new RelaySyncCommand(DownloadFromWebButtonCommandExecute);
            _pauseDownloadRelayButtonCommand = new RelaySyncCommand(PauseDownloadButtonCommandExecute);
            _cancelDownloadRelayButtonCommand = new RelaySyncCommand(CancelCownloadButtonCommandExecute);
            _clearDownloadListButtonCommand = new RelaySyncCommand(ClearDownloadButtonCommandExecute);
            _clearLogsListButtonCommand = new RelaySyncCommand(ClearLogsListButtonCommandExecute);

            DownloadsList.CollectionChanged += DownloadsListCollectionChangedEventHandler;
        }

        private void DownloadsListCollectionChangedEventHandler(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            
        }

        public ObservableCollection<DownloadAbstract> DownloadsList
        {
            get => AppSingleton.Instance.DownloadsList;
            set
            {
                AppSingleton.Instance.DownloadsList = value;
                OnPropertyChanged();
            }
        }

        public CustomDynamicLinkedList<IDebugInfo> Logs
        {
            get => AppSingleton.Instance.Logs;
        }

        #region RelayCommandsForButtons
        #region SyncCommands
        public RelaySyncCommand DownloadFromFTPRelayCommand
        {
            get => _downloadFromFTPButtonCommand;
            private set { _downloadFromFTPButtonCommand = value; }
        }

        public RelaySyncCommand DownloadFromWebRelayCommand
        {
            get => _downloadFromWebButtonCommand;
            private set { _downloadFromWebButtonCommand = value; }
        }

        public RelaySyncCommand PauseDownloadRelayCommand
        {
            get => _pauseDownloadRelayButtonCommand;
            private set { _pauseDownloadRelayButtonCommand = value; }
        }

        public RelaySyncCommand CancelDownloadRelayCommand
        {
            get => _cancelDownloadRelayButtonCommand;
            private set { _cancelDownloadRelayButtonCommand = value; }
        }

        public RelaySyncCommand ClearDownloadListRelayCommand
        {
            get => _clearDownloadListButtonCommand;
            private set { _clearDownloadListButtonCommand = value; }
        }

        public RelaySyncCommand ClearLogsListRelayCommand
        {
            get => _clearLogsListButtonCommand;
            private set { _clearLogsListButtonCommand = value; }
        }
        #endregion
        #region AsyncCommands
        #endregion
        #endregion

        #region RelayCommandsExecuteProcedures
        #region Sync
        void DownloadFromFTPButtonCommandExecute(object parameter)
        {
            
        }

        void DownloadFromWebButtonCommandExecute(object parameter)
        {
            if (parameter != null)
            {
                //Random rand = new Random();
                //int num = rand.Next();
                //DownloadFromInternet dl = new DownloadFromInternet(num, @"http://ipv4.download.thinkbroadband.com/20MB.zip", @$"C:\SWAP\test{num}.zip");
                //AppSingleton.Instance.DownloadsList.Add(dl);
                //dl.Download();

                //Logs.Add(DebugFactory.GetDebugInfo("New download started", $"{dl.DownloadID}"));

            }
            DownloadFromInternetWindow downloadFromInternetWindow = new DownloadFromInternetWindow(this);
            downloadFromInternetWindow.Show();
        }

        void PauseDownloadButtonCommandExecute(object parameter)
        {
            if (parameter != null)
            {
                DownloadFromInternet dl = (DownloadFromInternet)parameter;
                dl.PauseResumeDownload();
            }
        }

        void CancelCownloadButtonCommandExecute(object parameter)
        {
            if (parameter != null)
            {
                DownloadFromInternet dl = (DownloadFromInternet)parameter;
                dl.CancelRetryDownload();
            }
        }
        void ClearDownloadButtonCommandExecute(object parameter)
        {
            if (parameter != null)
            {
                DownloadsList = new ObservableCollection<DownloadAbstract>(AppSingleton.Instance.DownloadsList.Where(dl => dl.State != DownloadState.DownloadError && dl.State != DownloadState.DownloadCompleted && dl.State != DownloadState.DownloadCancelled));
            }
        }

        bool ClearDownloadButtonCommandCanExecute(object parameter)
        {
            return (AppSingleton.Instance.DownloadsList.Count == 0) || AppSingleton.Instance.DownloadsList.All((DownloadAbstract dl) => dl.State == DownloadState.DownloadCompleted);
        }

        void ClearLogsListButtonCommandExecute(object parameter)
        {
            Logs.Clear();
        }

        bool PauseDownloadButtonCommandCanExecute(object parameter)
        {
            if (parameter is null) return false;
            DownloadFromInternet dl = (DownloadFromInternet)parameter;
            return dl.Pausable;
        }
        #endregion
        #region Async
        #endregion
        #endregion
    }
}
