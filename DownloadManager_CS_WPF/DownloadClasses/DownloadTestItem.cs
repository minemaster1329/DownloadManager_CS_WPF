using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace DownloadManager_CS_WPF.DownloadClasses
{
    class DownloadTestItem : DownloadAbstract
    {
        public override void CancelRetryDownload()
        {
            Debug.WriteLine($"Download with ID {DownloadID} has been cancelled");
        }

        public override Task Download()
        {
            throw new NotImplementedException();
        }

        public override void PauseResumeDownload()
        {
            Debug.WriteLine($"Download with ID {DownloadID} has been paused");
        }

        public DownloadTestItem(int downloadID)
        {
            Pausable = false;
            DownloadSource = "Test Download Source";
            DownloadDestination = "Test DownloadDestination";
            DownloadID = downloadID;
            State = DownloadState.DownloadPending;
            Progress = 0;
        }
    }
}
