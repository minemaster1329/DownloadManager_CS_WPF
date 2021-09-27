using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace DownloadManager_CS_WPF.ViewModels
{
    class FTPCredentialsFormViewModel : PropertyChangedHandlerClass
    {
        private bool _notAnonymousAccess = true;
        public bool NotAnonymousAccess
        {
            get => _notAnonymousAccess;
            set
            {
                _notAnonymousAccess = value;
                OnPropertyChanged();
            }
        }

        public FTPCredentialsFormViewModel() { }
    }
}
