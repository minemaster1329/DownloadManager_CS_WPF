using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager_CS_WPF.DownloadClasses
{
    public enum DownloadState { DownloadPending, DownloadStarted, DownloadPaused, DownloadCancelled, DownloadError, DownloadCompleted };
    public abstract class DownloadAbstract : PropertyChangedHandlerClass
    {
        private int _downloadID;

        protected long _downloadSize = 0;
        protected long _downloaded = 0;
        
        private DownloadState _downloadState = DownloadState.DownloadPending;
        private bool _pausable = false;
        private int _progress = 0;

        public int Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                OnPropertyChanged();
            }
        }
        public bool Pausable
        {
            get => _pausable && ((State == DownloadState.DownloadStarted) || (State == DownloadState.DownloadPaused));
            protected set
            {
                _pausable = value;
                OnPropertyChanged();
            }
        }
        public int DownloadID
        {
            get => _downloadID;
            protected set
            {
                _downloadID = value;
                OnPropertyChanged();
            }
        }
        public DownloadState State
        {
            get => _downloadState;
            protected set
            {
                _downloadState = value;
                AppSingleton.Instance.DownloadReport[_downloadID].DownloadState = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Pausable));
            }
        }

        public string DownloadSource { get; protected set; }
        public string DownloadDestination { get; protected set; }

        public abstract Task Download();

        public abstract void PauseResumeDownload();
        public abstract void CancelRetryDownload();
    }
}
