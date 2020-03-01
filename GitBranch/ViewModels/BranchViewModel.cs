using GalaSoft.MvvmLight.Command;
using GitBranch.Common;
using Microsoft.Extensions.Options;
using System.Windows;
using System.Windows.Input;

namespace GitBranch.ViewModels
{
    public class BranchViewModel
    {
        private AppSettings _appSettings;
        public BranchViewModel(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string Name { get; set; }
        public bool IsRemote => IsRemoteInternal();
        public bool IsCurrentlyOn { get; set; }

        private bool IsRemoteInternal()
        {
            return Name.StartsWith(_appSettings.RemotePrefix);
        }

        public ICommand CopyCheckoutToClipboardCommand => new RelayCommand(CopyCheckoutToClipboard,
            () => true);

        private void CopyCheckoutToClipboard()
        {
            var checkoutGitCommand = $"git checkout -b \"{Name.Replace(_appSettings.RemotePrefix, "")}\" \"{Name}\"";
            Clipboard.SetText(checkoutGitCommand);
        }
    }
}
