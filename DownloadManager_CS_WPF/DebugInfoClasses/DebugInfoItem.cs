using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager_CS_WPF.DebugInfoClasses
{
    public enum DebugInfoType { Info, Warning, Error, DownloadCompleted };
    class DebugInfoItem
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime LogItemDate { get; set; }
        public DebugInfoType DebugItemType { get; set; }
        public bool Hidden { get; set; }
    }
}
