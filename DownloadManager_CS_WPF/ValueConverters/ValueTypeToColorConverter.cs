using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Diagnostics;

namespace DownloadManager_CS_WPF.ValueConverters
{
    class ValueTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                IDebugInfo debugInfo = (IDebugInfo)value;
                if (debugInfo is DebugInfo) return Brushes.Black;
                else if (debugInfo is DebugWarning) return Brushes.YellowGreen;
                else if (debugInfo is DebugError) return Brushes.Red;
                else if (debugInfo is DebugDownloadCompletedSuccesfully) return Brushes.Green;
                else return Brushes.Black;
            }
            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
