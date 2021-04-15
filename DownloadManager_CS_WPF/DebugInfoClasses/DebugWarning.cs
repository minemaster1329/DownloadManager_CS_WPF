﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadManager_CS_WPF
{
    public class DebugWarning : IDebugInfo
    {
        private string _title;
        private string _message;

        public string Title => _title;

        public string Message => _message;

        DebugWarning() { }

        public DebugWarning(string title, string message)
        {
            _title = title;
            _message = message;
        }

        public override string ToString() => $"{_title}: {_message}";
    }
}