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
        private bool _anonymousAccess = true;
        public bool AnonymousAccess
        {
            get => _anonymousAccess;
            set
            {
                _anonymousAccess = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CredentialsControlsEnabled));
            }
        }

        public bool CredentialsControlsEnabled
        {
            get => !_anonymousAccess;
        }

        public FTPCredentialsFormViewModel() { }
    }
}
