using GalaSoft.MvvmLight.Messaging;
using GitBranch.Common;
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
using System.Windows.Shapes;

namespace GitBranch.Views
{
    /// <summary>
    /// Interaction logic for ConfigDialog.xaml
    /// </summary>
    public partial class ConfigDialog : Window
    {
        public ConfigDialog()
        {
            InitializeComponent();
            Messenger.Default.Register<SaveConfigResult>(this, UnregisterAndClose);
            Messenger.Default.Register<CancelConfigResult>(this, UnregisterAndClose);
        }

        private void UnregisterAndClose(SaveConfigResult result)
        {
            Messenger.Default.Unregister<SaveConfigResult>(this);
            Close();
        }

        private void UnregisterAndClose(CancelConfigResult result)
        {
            Messenger.Default.Unregister<CancelConfigResult>(this);
            Close();
        }
    }
}
