using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Security;

using FluentFTP;

using DownloadManager_CS_WPF.DebugInfoClasses;

namespace DownloadManager_CS_WPF.FTPConnectionClasses
{
    public class FTPConnectionClass
    {
        readonly FtpClient _ftpClient;
        readonly CredentialsSet _credentials;

        public FtpClient Client
        {
            get => _ftpClient;
        }

        FTPConnectionClass(CredentialsSet credentials)
        {
            _ftpClient = new();
            _ftpClient.Host = credentials.Host;
            if (credentials.AnonymousAccess)
            {
                _ftpClient.Credentials = new NetworkCredential("anonymous", "janeDoe@contonso.com");
                _ftpClient.Connect();
                _credentials = credentials;
            }

            else
            {
                _ftpClient.Credentials = new NetworkCredential(credentials.UserName, credentials.Password);
                _ftpClient.Connect();
                _credentials = credentials;
            }
        }

        public static bool FTPCredentialsCorrect(ref CredentialsSet credentialsSet, out FTPConnectionClass ftp_client, out Exception exceptionType)
        {
            try
            {
                ftp_client = new(credentialsSet);
                exceptionType = null;
                return true;
            }
            catch (Exception e)
            {
                exceptionType = e;
                switch (e)
                {
                    case TimeoutException _:
                        AppSingleton.Instance.Logs.Add(DebugFactory.GetDebugError("Error when creating connection to FTP server", $"Connection timeout"));
                        break;
                    case FtpAuthenticationException _:
                        AppSingleton.Instance.Logs.Add(DebugFactory.GetDebugError("Error when creating connection to FTP server", "invalid credentials"));
                        break;
                    default:
                        AppSingleton.Instance.Logs.Add(DebugFactory.GetDebugError("Error when creating connection to FTP server", e.Message));
                        break;
                }
            }
            ftp_client = null;
            return false;
        }

        public FTPConnectionClass GetCopy()
        {
            return new FTPConnectionClass(_credentials);
        }
    }
}
