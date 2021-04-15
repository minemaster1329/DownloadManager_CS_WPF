using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadManager_CS_WPF
{
    public class DebugDownloadCompletedSuccesfully : IDebugInfo
    {
        private string _title;
        private string _message;

        public string Title => _title;

        public string Message => _message;

        DebugDownloadCompletedSuccesfully() { }

        public DebugDownloadCompletedSuccesfully(string title, string message)
        {
            _title = title;
            _message = message;
        }

        public override string ToString() => $"{_title}: {_message}";
    }
}
