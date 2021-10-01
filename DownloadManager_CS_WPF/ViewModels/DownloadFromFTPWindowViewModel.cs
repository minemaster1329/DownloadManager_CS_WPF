using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.Win32;

using DownloadManager_CS_WPF.Windows;
using DownloadManager_CS_WPF.FTPConnectionClasses;
using DownloadManager_CS_WPF.DownloadClasses;
using DownloadManager_CS_WPF.DebugInfoClasses;
using DownloadManager_CS_WPF.DownloadClasses.DownloadReportsClasses;

using FluentFTP;

namespace DownloadManager_CS_WPF.ViewModels
{
    class DownloadFromFTPWindowViewModel : PropertyChangedHandlerClass
    {
        private RelaySyncCommand _addNewFTPConnectionCommand;
        private RelaySyncCommand _refreshFilesList;
        private RelaySyncCommand _downloadFtpFile;

        private FTPConnectionClass _currentConnection;
        private string _searchString = "";

        public ObservableCollection<FtpTreeItem> TreeItems { get; init; }

        public DownloadFromFTPWindowViewModel()
        {
            TreeItems = new ObservableCollection<FtpTreeItem>();
            _addNewFTPConnectionCommand = new RelaySyncCommand(AddNewFTPConnetionCommandExecute);
            _refreshFilesList = new RelaySyncCommand(RefreshFilesListCommandExecute, RefreshFilesListCommandCanExecute);
            _downloadFtpFile = new RelaySyncCommand(DownloadFromFTPCommandExecute);
        }

        public ObservableCollection<FTPConnectionClass> ConnectionsList
        {
            get => AppSingleton.Instance.FTPConnectionsList;
        }

        public IList<FtpListItem> ItemsList
        {
            get
            {
                if (CurrentConnection != null)
                {
                    IList<FtpListItem> str = new List<FtpListItem>();
                    Debug.WriteLine("Fetching items list");
                    //str = CurrentConnection.GetCopy().Client.GetListing("", FtpListOption.AllFiles).Where(l => l.Type == FtpFileSystemObjectType.File).ToList();
                    str = CurrentConnection.GetCopy().Client.GetListing("", FtpListOption.AllFiles).ToList();
                    foreach (FtpListItem item in str)
                    {
                        Debug.WriteLine(item.FullName);
                    }
                    if (SearchString.Length > 0)
                    {
                        str = str.Where(st => st.Name.Contains(SearchString)).ToList();
                    }
                    return str;
                }
                else return null;
            }
        }

        public FTPConnectionClass CurrentConnection
        {
            get => _currentConnection;
            set
            {
                _currentConnection = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ItemsList));
            }
        }

        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ItemsList));
            }
        }

        public RelaySyncCommand AddNewFTPConnectionCommand
        {
            get => _addNewFTPConnectionCommand;
            private set
            {
                _addNewFTPConnectionCommand = value;
            }
        }

        public RelaySyncCommand RefreshItemsListCommand
        {
            get => _refreshFilesList;
            private set
            {
                _refreshFilesList = value;
            }
        }

        public RelaySyncCommand DownloadFromFTPCommand
        {
            get => _downloadFtpFile;
            set
            {
                _downloadFtpFile = value;
            }
        }

        private void AddNewFTPConnetionCommandExecute(object parameter)
        {
            FTPCredentialsForm fTPCredentialsForm = new();

            fTPCredentialsForm.ShowDialog();

            if (!fTPCredentialsForm.Cancelled)
            {
                FTPConnectionClass fTPConnectionClass = fTPCredentialsForm.GetFTPConnectionClass();
                if (!ConnectionsList.Any(x=>String.Compare(x.Client.Host, fTPConnectionClass.Client.Host)==0)) {
                    ConnectionsList.Add(fTPConnectionClass);
                }
            }
        }

        private void RefreshFilesListCommandExecute(object parameter)
        {
            OnPropertyChanged(nameof(ItemsList));
        }

        private bool RefreshFilesListCommandCanExecute(object parameter)
        {
            return (CurrentConnection != null);
        }

        private void DownloadFromFTPCommandExecute(object parameter)
        {
            Debug.WriteLine("context menu command called");
            if (parameter != null)
            {
                FtpListItem ftpListItem = parameter as FtpListItem;
                SaveFileDialog saveFileDialog = new();
                saveFileDialog.FileName = ftpListItem.Name;

                bool? dial_res = saveFileDialog.ShowDialog();

                if (dial_res == true && saveFileDialog.FileName != "")
                {
                    int d_id = 0;
                    while (AppSingleton.Instance.DownloadReport.ContainsKey(d_id)) d_id = AppSingleton.Instance.RandomGenerator.Next();

                    AppSingleton.Instance.AddNewDownloadReport(d_id, new DownloadFromFTPReportItem()
                    {
                        DownloadDate = DateTime.Now,
                        DownloadDestination = saveFileDialog.FileName,
                        DownloadServer = CurrentConnection.Client.Host,
                        DownloadSource=ftpListItem.FullName,
                        DownloadState=DownloadState.DownloadPending
                    });
                    DownloadFromFTP downloadFromFTP = new(CurrentConnection.GetCopy(), saveFileDialog.FileName, ftpListItem, d_id);
                    AppSingleton.Instance.DownloadsList.Add(downloadFromFTP);
                    downloadFromFTP.Download();
                    AppSingleton.Instance.Logs.Add(DebugFactory.GetDebugInfo("Downloading from FTP", ftpListItem.FullName));
                }
            }
        }
    }
}
