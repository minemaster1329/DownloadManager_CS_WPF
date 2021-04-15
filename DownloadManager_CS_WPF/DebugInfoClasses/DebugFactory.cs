using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadManager_CS_WPF
{
    public sealed class DebugFactory
    {
        public static DebugError GetDebugError(string title, string message) => new DebugError(title, message);
        public static DebugWarning GetDebugWarning(string title, string message) => new DebugWarning(title, message);
        public static DebugInfo GetDebugInfo(string title, string message) => new DebugInfo(title, message);
        public static DebugDownloadCompletedSuccesfully GetDebugDownloadCompletedSuccesfully(string title, string message) => new DebugDownloadCompletedSuccesfully(title, message);
    }
}
