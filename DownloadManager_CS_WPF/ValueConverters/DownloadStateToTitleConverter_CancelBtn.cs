using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

using DownloadManager_CS_WPF.DownloadClasses;

namespace DownloadManager_CS_WPF.ValueConverters
{
    class DownloadStateToTitleConverter_CancelBtn : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return "";
            else return value switch
            {
                DownloadState.DownloadCancelled => "Retry",
                DownloadState.DownloadCompleted => "Retry",
                _ => "Cancel"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
