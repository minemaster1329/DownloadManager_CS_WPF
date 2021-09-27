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
using System.Security;

using FluentFTP;

using DownloadManager_CS_WPF.FTPConnectionClasses;
using DownloadManager_CS_WPF.ViewModels;

namespace DownloadManager_CS_WPF.Windows
{
    /// <summary>
    /// Interaction logic for FTPCredentialsForm.xaml
    /// </summary>
    public partial class FTPCredentialsForm : Window
    {
        public bool Cancelled { get; private set; } = true;
        private FTPConnectionClass _ftpConnectionClass = null;
        public FTPCredentialsForm()
        {
            InitializeComponent();
            this.DataContext = new FTPCredentialsFormViewModel();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            CredentialsSet credentials = new(ServerNameInput.Text, UserNameInput.Text, PasswordInput.SecurePassword);

            if (FTPConnectionClass.FTPCredentialsCorrect(ref credentials, out _ftpConnectionClass, out Exception ex_type))
            {
                Cancelled = false;
                Close();
            }

            else
            {
                string message = ex_type switch
                {
                    TimeoutException _ => "Connection timeout. Check your internet connection or server configuration.",
                    FtpAuthenticationException _ => "Invalid user or password.",
                    _ => ex_type.Message,
                };
                MessageBox.Show(this ,message, $"Error when connecting to {ServerNameInput.Text}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public FTPConnectionClass GetFTPConnectionClass()
        {
            if (Cancelled) return null;
            else return _ftpConnectionClass;
        }
    }
}
