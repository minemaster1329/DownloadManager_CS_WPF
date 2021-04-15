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
using DownloadManager_CS_WPF.ViewModels;
using DownloadManager_CS_WPF.DownloadClasses;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Linq;

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
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                string file_name = link.Split('/').Last();
                string extension = link.Split('.').Last();
                saveFileDialog.AddExtension = true;
                saveFileDialog.DefaultExt = extension;
                saveFileDialog.ValidateNames = true;
                saveFileDialog.FileName = file_name;

                bool? dial_res = saveFileDialog.ShowDialog(this);

                if (dial_res == true && saveFileDialog.FileName != "")
                {
                    DownloadFromInternet dl = new DownloadFromInternet(_rnd.Next(), link, saveFileDialog.FileName);
                    _mainWindowViewModel.DownloadsList.Add(dl);
                    dl.Download();
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
