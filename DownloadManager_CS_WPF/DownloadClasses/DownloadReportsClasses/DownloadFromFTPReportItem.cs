using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager_CS_WPF.DownloadClasses.DownloadReportsClasses
{
    class DownloadFromFTPReportItem : IDownloadReport
    {
        public DownloadState DownloadState { get; set; }
        public string DownloadSource { get; init; }
        public string DownloadDestination { get; init; }
        public string DownloadServer { get; init; }
        public DateTime DownloadDate { get; init; }
    }
}
