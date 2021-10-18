using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

using Microsoft.Win32;

using DownloadManager_CS_WPF.ViewModels;
using DownloadManager_CS_WPF.DownloadClasses;
using DownloadManager_CS_WPF.DownloadClasses.DownloadReportsClasses;

namespace DownloadManager_CS_WPF.Windows
{
    /// <summary>
    /// Interaction logic for DownloadFromInternetWindow.xaml
    /// </summary>
    public partial class DownloadFromInternetWindow : Window
    {
        MainWindowViewModel _mainWindowViewModel;
        Random _rnd;

        DownloadFromInternetWindow()
        {
            InitializeComponent();
            _mainWindowViewModel = null;
            _rnd = null;
        }

        public DownloadFromInternetWindow(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            _mainWindowViewModel = mainWindowViewModel;
            _rnd = new Random();
        }

        private async void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            string link = LinkInput.Text;

            if (await DownloadFromInternet.ValidateDownload(link))
            {
                SaveFileDialog saveFileDialog = new();
                string file_name = link.Split('/').Last();
                string extension = link.Split('.').Last();
                saveFileDialog.AddExtension = true;
                saveFileDialog.DefaultExt = extension;
                saveFileDialog.ValidateNames = true;
                saveFileDialog.FileName = file_name;

                bool? dial_res = saveFileDialog.ShowDialog(this);

                if (dial_res == true && saveFileDialog.FileName != "")
                {
                    int d_id = 0;
                    while (AppSingleton.Instance.DownloadReport.ContainsKey(d_id)) d_id = AppSingleton.Instance.RandomGenerator.Next(); 
                    AppSingleton.Instance.AddNewDownloadReport(d_id, new DownloadFromWebReportItem()
                    {
                        DownloadDate = DateTime.Now,
                        DownloadDestination = saveFileDialog.FileName,
                        DownloadSource=link,
                        DownloadState=DownloadState.DownloadPending
                    });
                    DownloadFromInternet dl = new(d_id, link, saveFileDialog.FileName);
                    _mainWindowViewModel.DownloadsList.Add(dl);
                    dl.Download();
                    //AppSingleton.Instance.Logs.Add(DebugFactory.GetDebugInfo("Download started", $"From {dl.DownloadSource}"));
                    CustomLoggerSingleton.Instance.AddNewLog("Download from Internet", $"Starting download from {dl.DownloadSource}", LogType.Info);
                }
            }
            else
            {
                MessageBox.Show("URL not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
