using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadManager_CS_WPF.DebugInfoClasses
{
    public sealed class DebugFactory
    {
        public static DebugError GetDebugError(string title, string message) => new() { Title = title, Message = message, DebugItemType = DebugInfoType.Error, Hidden = false, LogItemDate = DateTime.Now };
        public static DebugWarning GetDebugWarning(string title, string message) => new() { Title = title, Message = message, DebugItemType = DebugInfoType.Warning, Hidden = false, LogItemDate = DateTime.Now };
        public static DebugInfo GetDebugInfo(string title, string message) => new() { Title = title, Message = message, DebugItemType=DebugInfoType.Info, Hidden=false, LogItemDate=DateTime.Now };
        public static DebugDownloadCompletedSuccesfully GetDebugDownloadCompletedSuccesfully(string title, string message) => new() { Title = title, Message = message, DebugItemType = DebugInfoType.DownloadCompleted, Hidden = false, LogItemDate = DateTime.Now };
    }
}
