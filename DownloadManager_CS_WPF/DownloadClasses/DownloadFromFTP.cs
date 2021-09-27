using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Threading;

using FluentFTP;

using DownloadManager_CS_WPF.DebugInfoClasses;
using DownloadManager_CS_WPF.FTPConnectionClasses;

namespace DownloadManager_CS_WPF.DownloadClasses
{
    class DownloadFromFTP : DownloadAbstract
    {
        private readonly FTPConnectionClass _ftpClient;
        private readonly FtpListItem _ftpListItem;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public override void CancelRetryDownload()
        {
            _cancellationTokenSource.Cancel(true);
        }

        public override async Task Download()
        {
            if (State == DownloadState.DownloadCompleted) return;
            try
            {
                using FileStream fs = new(DownloadDestination, FileMode.OpenOrCreate);
                Progress<FtpProgress> progress_fun = new(x =>
                {
                    Progress = (int)x.Progress;
                });
                State = DownloadState.DownloadStarted;
                Debug.WriteLine("Starting download");
                bool downloadedSuccessfully = await _ftpClient.Client.DownloadAsync(fs, _ftpListItem.FullName, 0, progress_fun, _cancellationTokenSource.Token);
                State = DownloadState.DownloadCompleted;
                AppSingleton.Instance.Logs.Add(DebugFactory.GetDebugDownloadCompletedSuccesfully($"Downloading from FTP completed", DownloadSource));
            }
            catch (TaskCanceledException)
            {
                State = DownloadState.DownloadCancelled;
                AppSingleton.Instance.Logs.Add(DebugFactory.GetDebugWarning($"Download cancelled", $"{_ftpClient.Client.Host}/{_ftpListItem.FullName}"));
            }
            catch (Exception e)
            {
                State = DownloadState.DownloadError;
                AppSingleton.Instance.Logs.Add(DebugFactory.GetDebugError($"Error when downloading from FTP {DownloadSource}", e.Message));
            }
        } 

        public override void PauseResumeDownload()
        {
            throw new NotImplementedException();
        }

        public DownloadFromFTP(FTPConnectionClass ftp_client, string destination, FtpListItem ftpListItem, int id)
        {
            _ftpClient = ftp_client;
            _ftpListItem = ftpListItem;
            _cancellationTokenSource = new CancellationTokenSource();

            DownloadSource = _ftpListItem.FullName;
            DownloadDestination = destination;
            DownloadID = id;
            Pausable = false;
            State = DownloadState.DownloadPending;
        }
    }
}
