using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DownloadManager_CS_WPF;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Data;

using System.Threading.Tasks;
using System.Threading;

using DownloadManager_CS_WPF.DownloadClasses;
using DownloadManager_CS_WPF.Windows;

namespace DownloadManager_CS_WPF.ViewModels
{
    public partial class MainWindowViewModel : PropertyChangedHandlerClass
    {
        #region RelayCommandsDefinitions
        #region Sync
        RelaySyncCommand _downloadFromFTPButtonCommand;
        RelaySyncCommand _downloadFromWebButtonCommand;
        RelaySyncCommand _pauseDownloadRelayButtonCommand;
        RelaySyncCommand _cancelDownloadRelayButtonCommand;
        RelaySyncCommand _clearDownloadListButtonCommand;
        RelaySyncCommand _clearLogsListButtonCommand;
        RelaySyncCommand _showScheduledDownloadsCommand;
        RelaySyncCommand _scheduleNewWebDownloadCommand;
        RelaySyncCommand _scheduleNewFtpDownloadCommand;
        RelaySyncCommand _showDownloadsHistory;
        #endregion
        #region Async
        #endregion
        #endregion

        public MainWindowViewModel()
        {
            _downloadFromFTPButtonCommand = new RelaySyncCommand(DownloadFromFTPButtonCommandExecute);
            _downloadFromWebButtonCommand = new RelaySyncCommand(DownloadFromWebButtonCommandExecute);
            _pauseDownloadRelayButtonCommand = new RelaySyncCommand(PauseDownloadButtonCommandExecute, PauseDownloadButtonCommandCanExecute);
            _cancelDownloadRelayButtonCommand = new RelaySyncCommand(CancelDownloadButtonCommandExecute, CancelDownloadButtonCanExecute);
            _clearDownloadListButtonCommand = new RelaySyncCommand(ClearDownloadButtonCommandExecute, ClearDownloadButtonCommandCanExecute);
            _clearLogsListButtonCommand = new RelaySyncCommand(ClearLogsListButtonCommandExecute);
            
            _showScheduledDownloadsCommand = new RelaySyncCommand(ShowScheduledDownloadsMenuItemCommandExecute);
            _scheduleNewFtpDownloadCommand = new RelaySyncCommand(ScheduleNewFTPdownloadCommandExecute);
            _scheduleNewWebDownloadCommand = new RelaySyncCommand(ScheduleNewWebDownloadCommandExecute);
            _showDownloadsHistory = new RelaySyncCommand(ShowDownloadsHistoryCommandExecute);
        }

        public ObservableCollection<DownloadAbstract> DownloadsList
        {
            get => AppSingleton.Instance.DownloadsList;
        }

        #region Main Window ViewModel Fields (w/o relay commands)
        public ObservableCollection<string> AppLogs
        {
            get => CustomLoggerSingleton.Instance.Logs;
        }
        #endregion
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

        public RelaySyncCommand ShowScheduledDownloadsCommand
        {
            get => _showScheduledDownloadsCommand;
            init => _showScheduledDownloadsCommand = value;
        }

        public RelaySyncCommand ScheduleNewWebDownloadCommand
        {
            get => _scheduleNewWebDownloadCommand;
            init => _scheduleNewWebDownloadCommand = value;
        }

        public RelaySyncCommand ScheduleNewFtpDownloadCommand
        {
            get => _scheduleNewFtpDownloadCommand;
            init => _scheduleNewFtpDownloadCommand = value;
        }

        public RelaySyncCommand ShowDownloadsHistoryCommand
        {
            get => _showDownloadsHistory;
            init => _showScheduledDownloadsCommand = value;
        }
        #endregion
        #region AsyncCommands
        #endregion
        #endregion
    }
}
