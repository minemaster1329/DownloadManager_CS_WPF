using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Linq;

using DownloadManager_CS_WPF.DownloadClasses;

namespace DownloadManager_CS_WPF.ValueConverters
{
    class DownloadsListToMainWindowTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return "NA";
            else
            {
                ObservableCollection<DownloadAbstract> coll =(ObservableCollection<DownloadAbstract>) value;

                int downloading_files = 0;
                if (coll.Count == 0 || (downloading_files = coll.Where(x => x.State == DownloadState.DownloadStarted).Count()) == 0) return "Download Manager: C# WPF";
                else return $"Downloading {coll.Where(x => x.State == DownloadState.DownloadStarted).Count()} files.";
            }
        } 

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
