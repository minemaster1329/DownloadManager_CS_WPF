using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager_CS_WPF.ScheduledDownloadClasses
{
    public interface IScheduledDownload
    {
        string DownloadSource { get; set; }
        string DownloadDestination { get; set; }
        DateTime DownloadDate { get; set; }

        public RelaySyncCommand DeleteCommand { get; init; }
        public RelaySyncCommand EditCommand { get; init; }

        void DeleteCommandButtonExecute(object parameter);
        void EditCommandButtonExecute(object parameter);
    }
}
