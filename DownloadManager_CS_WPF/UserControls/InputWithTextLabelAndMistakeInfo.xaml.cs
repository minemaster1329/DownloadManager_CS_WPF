using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DownloadManager_CS_WPF.ViewModels;

namespace DownloadManager_CS_WPF.UserControls
{
    /// <summary>
    /// Interaction logic for InputWithTextLabelAndMistakeInfo.xaml
    /// </summary>
    public partial class InputWithTextLabelAndMistakeInfo : UserControl
    {
        public static readonly DependencyProperty labelTextDependencyProperty = DependencyProperty.Register("LabelText", typeof(string), typeof(InputWithTextLabelAndMistakeInfo), new FrameworkPropertyMetadata("DefaultLabel"));

        public InputWithTextLabelAndMistakeInfo()
        {
            InitializeComponent();
            DataContext = new InputWithTextLabelAndMistakeInfoViewModel(); 
        }

        public string LabelText
        {
            get => (string)GetValue(labelTextDependencyProperty);
            set
            {
                SetValue(labelTextDependencyProperty, value);
            }
        }

        public string InputText => ((InputWithTextLabelAndMistakeInfoViewModel)this.DataContext).GetInputText();
    }
}
