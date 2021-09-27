using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager_CS_WPF.ScheduledDownloadClasses
{
    class ScheduledDownloadFromWeb : IScheduledDownload
    {
        public string DownloadSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string DownloadDestination { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime DownloadDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public RelaySyncCommand DeleteCommand { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
        public RelaySyncCommand EditCommand { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }

        public void DeleteCommandButtonExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void EditCommandButtonExecute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
