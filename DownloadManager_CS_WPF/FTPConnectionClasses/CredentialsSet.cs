using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Runtime.InteropServices;

namespace DownloadManager_CS_WPF.FTPConnectionClasses
{
    public record CredentialsSet(bool AnonymousAccess, string Host, string UserName, SecureString Password)
    {
        public bool AnonymousAccess { get; init; } = AnonymousAccess;
        public string Host { get; init; } = Host;
        public string UserName { get; init; } = UserName;
        public SecureString Password { get; init; } = Password;
    }
}
