using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager_CS_WPF.DownloadClasses.DownloadReportsClasses
{
    public interface IDownloadReport
    {
        DownloadState DownloadState { get; set; }
        string DownloadSource { get; init; }
        string DownloadDestination { get; init; }
        DateTime DownloadDate { get; init; }
    }
}
