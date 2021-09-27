using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using DownloadManager_CS_WPF.ViewModels;

namespace DownloadManager_CS_WPF.Windows
{
    /// <summary>
    /// Interaction logic for ShowDownloadsReportWindow.xaml
    /// </summary>
    public partial class ShowDownloadsReportWindow : Window
    {
        public ShowDownloadsReportWindow()
        {
            InitializeComponent();
            DataContext = new ShowDownloadsReportWindowViewModel();
        }
    }
}
