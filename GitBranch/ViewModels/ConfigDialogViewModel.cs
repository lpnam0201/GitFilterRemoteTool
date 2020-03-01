using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GitBranch.Common;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;

namespace GitBranch.ViewModels
{
    public class ConfigDialogViewModel : ViewModelBase
    {
        private string _remotePrefix;
        public string RemotePrefix
        {
            get { return _remotePrefix; }
            set
            {
                _remotePrefix = value;
                RaisePropertyChanged(nameof(RemotePrefix));
            }
        }

        private string _sourceDirectory;
        public string SourceDirectory
        {
            get { return _sourceDirectory; }
            set
            {
                _sourceDirectory = value;
                RaisePropertyChanged(nameof(SourceDirectory));
            }
        }

        public ICommand SaveCommand => new RelayCommand(Save);
        public ICommand CancelCommand => new RelayCommand(Cancel);

        public ConfigDialogViewModel()
        {
            
        }

        private void Save()
        {
            var appSettings = new AppSettings
            {
                CallGitFetchBeforeSearch = true,
                RemotePrefix = RemotePrefix,
                SourceDirectory = SourceDirectory
            };

            var directory = AppDomain.CurrentDomain.BaseDirectory;
            var appSettingsStr = JsonConvert.SerializeObject(appSettings);

            File.WriteAllText(Path.Combine(directory, Constants.AppSettingsFileName), appSettingsStr);
            MessengerInstance.Send<SaveConfigResult>(null);
        }

        private void Cancel()
        {
            MessengerInstance.Send<CancelConfigResult>(null);
        }
    }
}
