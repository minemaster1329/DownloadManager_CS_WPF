using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager_CS_WPF.ScheduledDownloadClasses
{
    class ScheduledDownloadFromFTP : IScheduledDownload
    {
        public string DownloadSource { get; set; }
        public string DownloadDestination { get; set; }
        public string DownloadServer { get; set; }
        public DateTime DownloadDate { get; set; }
        public RelaySyncCommand DeleteCommand { get; init; }
        public RelaySyncCommand EditCommand { get; init; }

        public void DeleteCommandButtonExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void EditCommandButtonExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public ScheduledDownloadFromFTP()
        {
            DownloadSource = "FTP download source";
            DownloadServer = "FTP download server";
            DownloadDestination = "FTP download destination";
            DownloadDate = DateTime.Now;
        }
    }
}
