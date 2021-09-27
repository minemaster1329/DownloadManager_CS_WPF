using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Diagnostics;

using DownloadManager_CS_WPF.DebugInfoClasses;

namespace DownloadManager_CS_WPF.ValueConverters
{
    class ValueTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return value switch
                {
                    DebugInfo _ => Brushes.Black,
                    DebugWarning _ => Brushes.YellowGreen,
                    DebugError _ => Brushes.Red,
                    DebugDownloadCompletedSuccesfully _ => Brushes.Green,
                    _ => throw new ArgumentException("Invalid input exception"),
                };
            }
            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
