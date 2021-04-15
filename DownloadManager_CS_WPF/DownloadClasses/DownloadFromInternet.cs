﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace DownloadManager_CS_WPF.DownloadClasses
{
    class DownloadFromInternet : DownloadAbstract
    {
        HttpClient _httpClient;
        const int CHUNK_SIZE = 5120; 
        public DownloadFromInternet(int id, string source, string destination)
        {
            DownloadID = id;
            DownloadSource = source;
            DownloadDestination = destination;
            Pausable = false;

            State = DownloadState.DownloadPending;

            _httpClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Other");
        }

        public override void CancelRetryDownload()
        {
            if (State == DownloadState.DownloadCancelled || State == DownloadState.DownloadError)
            {
                State = DownloadState.DownloadStarted;
                _downloaded = 0;
                StartDownload();
                AppSingleton.Instance.Logs.Add(DebugFactory.GetDebugInfo($"Download {DownloadID}", "retrying download"));
            }

            else if (State == DownloadState.DownloadStarted || State == DownloadState.DownloadPaused)
            {
                State = DownloadState.DownloadCancelled;
                _downloaded = 0;
                Pausable = false;
                Progress = 0;
                AppSingleton.Instance.Logs.Add(DebugFactory.GetDebugInfo($"Download {DownloadID}", "download cancelled"));
            }
        }

        public override async Task Download()
        {
            if (State == DownloadState.DownloadCompleted) return;
            try
            {
                Task<bool> t_pausable = CheckIfPausable();
                Task<long> t_length = GetFileSize();

                Task.WaitAll(t_pausable, t_length);
                Pausable = t_pausable.Result || Pausable;
                _downloadSize = t_length.Result;

                if (_downloadSize > 0)
                {
                    State = DownloadState.DownloadStarted;
                    if (!Pausable) AppSingleton.Instance.Logs.Add(DebugFactory.GetDebugWarning($"Download {DownloadID}", "this download cannot be paused"));
                    await StartDownload();
                }
                else State = DownloadState.DownloadError;
            }
            catch (Exception e)
            {
                State = DownloadState.DownloadError;
                AppSingleton.Instance.Logs.Add(DebugFactory.GetDebugError($"Error when download {DownloadID}", e.Message));
            }
        }

        public override void PauseResumeDownload()
        {
            if (State == DownloadState.DownloadPaused)
            {
                State = DownloadState.DownloadStarted;
                StartDownload();
                AppSingleton.Instance.Logs.Add(DebugFactory.GetDebugInfo($"Download {DownloadID}", "download resumed"));
            }
            else if (State == DownloadState.DownloadStarted && Pausable)
            {
                State = DownloadState.DownloadPaused;
                AppSingleton.Instance.Logs.Add(DebugFactory.GetDebugInfo($"Download {DownloadID}", "download paused"));
            }
        }

        public static async Task<bool> ValidateDownload(string source)
        {
            try
            {
                using HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Other");
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Head, source);
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                return httpResponseMessage.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                AppSingleton.Instance.Logs.Add(DebugFactory.GetDebugError("Error when trying to download", e.Message));
                return false;
            }
        }

        protected Task<long> GetFileSize() => Task<long>.Factory.StartNew(() =>
        {
            long output = -1;
            try
            {
                using WebClient wc = new WebClient();
                wc.Headers.Add("User-Agent: Other");
                wc.OpenRead(new Uri(DownloadSource));
                output = long.Parse(wc.ResponseHeaders["Content-Length"]);
            }
            catch (Exception e)
            {
                AppSingleton.Instance.Logs.Add(DebugFactory.GetDebugError("Error when trying to download", e.Message));
                output = -1;
            }
            return output;
        });

        protected Task<bool> CheckIfPausable() => Task<bool>.Factory.StartNew(() =>
         {
             try
             {
                 HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Head, DownloadSource);
                 HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                 _httpClient.SendAsync(httpRequestMessage).ContinueWith(async resp_mess => httpResponseMessage = await resp_mess).Wait();
                 if (httpResponseMessage.Headers.Contains("Accept-Ranges"))
                 {
                     return httpResponseMessage.Headers.Where(h => h.Key == "Accept-Ranges").First().Value.Contains("bytes");
                 }
                 else return false;
             }

             catch (Exception e)
             {
                 AppSingleton.Instance.Logs.Add(DebugFactory.GetDebugError("Error when cheching if download can be paused", e.Message));
                 return false;
             }
         });

        private async Task StartDownload()
        {
            using (Stream str = await _httpClient.GetStreamAsync(DownloadSource))
            {
                using (FileStream fs = new FileStream(DownloadDestination, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    while ((State == DownloadState.DownloadStarted) && _downloaded <= _downloadSize)
                    {
                        byte[] buffer = new byte[CHUNK_SIZE];
                        int bytes_read = await str.ReadAsync(buffer, 0, buffer.Length);

                        if (bytes_read == 0) break;

                        await fs.WriteAsync(buffer, 0, bytes_read);
                        _downloaded += bytes_read;
                        Progress = (int)(((double)_downloaded) / _downloadSize * 100);
                    }

                    if (_downloaded == _downloadSize)
                    {
                        State = DownloadState.DownloadCompleted;
                        AppSingleton.Instance.Logs.Add(DebugFactory.GetDebugDownloadCompletedSuccesfully($"Download {DownloadID}", "download completed successfully"));
                    }
                    else
                    {
                        if (State == DownloadState.DownloadPaused)
                        {
                            _httpClient.DefaultRequestHeaders.Remove("Range");
                            _httpClient.DefaultRequestHeaders.Add("Range", $"bytes={_downloaded}-{_downloadSize}");
                        }
                    }

                    await fs.FlushAsync();
                }
                if (State == DownloadState.DownloadCancelled)
                {
                    if (File.Exists(DownloadDestination)) File.Delete(DownloadDestination);
                    _httpClient.DefaultRequestHeaders.Remove("Range");
                }
            }
        }
    }
}
