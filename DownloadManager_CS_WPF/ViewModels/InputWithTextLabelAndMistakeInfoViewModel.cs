using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadManager_CS_WPF.ViewModels
{
    class InputWithTextLabelAndMistakeInfoViewModel : PropertyChangedHandlerClass
    {
        public string InputText { get; set; }

        public string ErrorLabelText { get; private set; }

        public string GetInputText() => InputText;

        public bool GetInputTextCorrect()
        {
            return true;
        }
    }
}
