using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Diagnostics;
using DownloadManager_CS_WPF.DownloadClasses;

namespace DownloadManager_CS_WPF.ValueConverters
{
    class DownloadButtonToPauseResumeButtonTitle : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return "NA";
            else
            {
                DownloadState dls = (DownloadState)value;
                return (dls == DownloadState.DownloadPaused) ? "Resume" : "Pause";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
