using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DownloadManager_CS_WPF.DownloadClasses.DownloadReportsClasses;

namespace DownloadManager_CS_WPF.ViewModels
{
    class ShowDownloadsReportWindowViewModel : PropertyChangedHandlerClass
    {
        RelaySyncCommand _exportToCSVCommand;
        RelaySyncCommand _exportToXMLCommand;
        RelaySyncCommand _exportToJSONCommand;

        public Dictionary<int, IDownloadReport> DownloadsReports => AppSingleton.Instance.DownloadReport;
        
        public ShowDownloadsReportWindowViewModel()
        {
            _exportToCSVCommand = new RelaySyncCommand(ExportToCSVCommandExecute);
            _exportToJSONCommand = new RelaySyncCommand(ExportToJSONCommandExecute);
            _exportToXMLCommand = new RelaySyncCommand(ExportToXMLCommandExecute);
        }

        RelaySyncCommand ExportToCSVCommand => _exportToCSVCommand;
        RelaySyncCommand ExportToXMLCommand => _exportToXMLCommand;
        RelaySyncCommand ExportToJSONCommand => _exportToJSONCommand;

        private void ExportToCSVCommandExecute(object parameter)
        {

        }

        private void ExportToXMLCommandExecute(object parameter)
        {

        }

        private void ExportToJSONCommandExecute(object parameter)
        {

        }
    }
}
