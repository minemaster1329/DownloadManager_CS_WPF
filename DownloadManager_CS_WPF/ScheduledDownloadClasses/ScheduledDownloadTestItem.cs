using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;

namespace DownloadManager_CS_WPF.ScheduledDownloadClasses
{
    class ScheduledDownloadTestItem : IScheduledDownload
    {
        RelaySyncCommand _deleteCommand;
        RelaySyncCommand _editCommand;

        public string DownloadSource { get; set; }
        public string DownloadDestination { get; set; }
        public DateTime DownloadDate { get; set; }
        public RelaySyncCommand DeleteCommand { get => _deleteCommand; init => _deleteCommand = value; }
        public RelaySyncCommand EditCommand { get => _editCommand; init => _editCommand = value; }

        public ScheduledDownloadTestItem(string source, string destination, DateTime downloadDate)
        {
            _deleteCommand = new RelaySyncCommand(DeleteCommandButtonExecute);
            _editCommand = new RelaySyncCommand(EditCommandButtonExecute);

            DownloadSource = source;
            DownloadDestination = destination;
            DownloadDate = downloadDate;
        }
        public void DeleteCommandButtonExecute(object parameter)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Do you want to remove download?", "Removing scheduled download", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);

            if (dialogResult == MessageBoxResult.Yes) AppSingleton.Instance.ScheduledDownloadsList.Remove(this);
        }
        public void EditCommandButtonExecute(object parameter)
        {
            MessageBox.Show("Edit Command Executed");
        }
    }
}
