using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;

using DownloadManager_CS_WPF.ScheduledDownloadClasses;
using DownloadManager_CS_WPF.Windows;

namespace DownloadManager_CS_WPF.ViewModels
{
    class ScheduledDownloadsWindowViewModel : PropertyChangedHandlerClass
    {
        RelaySyncCommand _newWebDownloadCommand;
        RelaySyncCommand _newFtpdownloadCommand;

        public ObservableCollection<IScheduledDownload> ScheduledDownloads => AppSingleton.Instance.ScheduledDownloadsList;

        public RelaySyncCommand NewWebDownloadCommand => _newWebDownloadCommand;
        public RelaySyncCommand NewFtpDownloadCommand => _newFtpdownloadCommand;

        public ScheduledDownloadsWindowViewModel()
        {
            _newFtpdownloadCommand = new RelaySyncCommand(NewFTPDownloadCommandExecute);
            _newWebDownloadCommand = new RelaySyncCommand(NewWebDownloadCommandExecute);
        }

        void NewWebDownloadCommandExecute(object parameter)
        {
            ScheduleNewWebDownloadWindow scheduleNewWebDownloadWindow = new ScheduleNewWebDownloadWindow();
            bool? dialogResult = scheduleNewWebDownloadWindow.ShowDialog();

            if (dialogResult is not null && dialogResult.Value)
            {

            }
        }

        void NewFTPDownloadCommandExecute(object parameter)
        {
            ScheduleNewFTPDownloadWindow scheduleNewFTPDownloadWindow = new ScheduleNewFTPDownloadWindow();
            bool? dialogResult = scheduleNewFTPDownloadWindow.ShowDialog();

            if (dialogResult is not null && dialogResult.Value)
            {

            }
        }
    }
}
