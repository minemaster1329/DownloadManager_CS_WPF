using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DownloadManager_CS_WPF;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Diagnostics;

using System.Threading.Tasks;
using System.Threading;

using DownloadManager_CS_WPF.DownloadClasses;
using DownloadManager_CS_WPF.Windows;

namespace DownloadManager_CS_WPF.ViewModels
{
    public partial class MainWindowViewModel : PropertyChangedHandlerClass
    {
        #region RelayCommandsExecuteProcedures
        #region Sync
        void DownloadFromFTPButtonCommandExecute(object parameter)
        {
            DownloadFromFTPWindow downloadFromFTPWindow = new();
            downloadFromFTPWindow.ShowDialog();
        }

        void DownloadFromWebButtonCommandExecute(object parameter)
        {
            DownloadFromInternetWindow downloadFromInternetWindow = new(this);
            downloadFromInternetWindow.ShowDialog();
        }

        void PauseDownloadButtonCommandExecute(object parameter)
        {
            if (parameter != null)
            {
                DownloadAbstract dl = (DownloadAbstract)parameter;
                if (dl is DownloadTestItem) dl.Progress++;
                else dl.PauseResumeDownload();
            }
        }

        void CancelDownloadButtonCommandExecute(object parameter)
        {
            if (parameter != null)
            {
                DownloadAbstract dl = (DownloadAbstract)parameter;
                if (dl is DownloadTestItem) dl.Progress = 0;
                else dl.CancelRetryDownload();
            }
        }
        void ClearDownloadButtonCommandExecute(object parameter)
        {
            if (parameter != null)
            {
                DownloadsList.RemoveAllOnCondition(item => item.State == DownloadState.DownloadCompleted || item.State == DownloadState.DownloadCancelled || item.State == DownloadState.DownloadError);
            }
        }

        void ClearLogsListButtonCommandExecute(object parameter)
        {
            //foreach (DebugInfoAbstract debugInfo in AppSingleton.Instance.Logs)
            //{
            //    debugInfo.Hidden = true;
            //}
            //OnPropertyChanged(nameof(LogsCollectionView));
            CustomLoggerSingleton.Instance.AddNewLog("XD", "XD", LogType.Error);
        }

        void ShowScheduledDownloadsMenuItemCommandExecute(object parameter)
        {
            ScheduledDownloadsWindow scheduledDownloadsWindow = new ScheduledDownloadsWindow();
            scheduledDownloadsWindow.ShowDialog();
        }

        void ScheduleNewWebDownloadCommandExecute(object parameter)
        {
            ScheduleNewWebDownloadWindow scheduleNewWebDownloadWindow = new ScheduleNewWebDownloadWindow();
            bool? dialogResult = scheduleNewWebDownloadWindow.ShowDialog();

            if (dialogResult is not null && dialogResult.Value)
            {

            }
        }

        void ScheduleNewFTPdownloadCommandExecute(object parameter)
        {
            ScheduleNewFTPDownloadWindow scheduleNewFTPDownloadWindow = new ScheduleNewFTPDownloadWindow();
            bool? dialogResult = scheduleNewFTPDownloadWindow.ShowDialog();

            if (dialogResult is not null && dialogResult.Value)
            {

            }
        }

        void ShowDownloadsHistoryCommandExecute(object parameter)
        {
            ShowDownloadsReportWindow showDownloadsReportWindow = new ShowDownloadsReportWindow();
            showDownloadsReportWindow.ShowDialog();
        }
        #region CanExecuteFunctions
        bool ClearDownloadButtonCommandCanExecute(object parameter)
        {
            return AppSingleton.Instance.DownloadsList.All((DownloadAbstract dl) => dl.State == DownloadState.DownloadCompleted || dl.State == DownloadState.DownloadCancelled || dl.State == DownloadState.DownloadError);
        }

        bool PauseDownloadButtonCommandCanExecute(object parameter)
        {
            if (parameter is null) return false;
            DownloadAbstract dl = (DownloadAbstract)parameter;
            return dl.Pausable;
        }

        bool CancelDownloadButtonCanExecute(object parameter)
        {
            if (parameter is null) return false;
            DownloadAbstract dl = (DownloadAbstract)parameter;
            return dl.State != DownloadState.DownloadCompleted;
        }
        #endregion
        #endregion
        #region Async
        #endregion
        #endregion
    }
}
