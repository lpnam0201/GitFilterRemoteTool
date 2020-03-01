using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GitBranch.Common;
using GitBranch.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GitBranch.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ISearchService _searchService;
        private IOptions<AppSettings> _appSettingOptions;

        public MainViewModel(ISearchService searchService, IOptions<AppSettings> appSettingOptions)
        {
            _searchService = searchService;
            _appSettingOptions = appSettingOptions;
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                RaisePropertyChanged(nameof(SearchText));
            }
        }

        private bool _isSearching;
        public bool IsSearching
        {
            get { return _isSearching; }
            set
            {
                _isSearching = value;
                RaisePropertyChanged(nameof(IsSearching));
            }
        }

        private ObservableCollection<BranchViewModel> _branches;
        public ObservableCollection<BranchViewModel> Branches
        { 
            get { return _branches; }
            set
            {
                _branches = value;
                RaisePropertyChanged(nameof(Branches));
            }
        }

        private BranchViewModel _currentBranch;
        public BranchViewModel CurrentBranch
        {
            get { return _currentBranch; }
            set
            {
                _currentBranch = value;
                RaisePropertyChanged("CurrentBranch");
            }
        }

        public ICommand SearchCommand => new RelayCommand(async () => await Search(), 
            () => !IsSearching && !String.IsNullOrEmpty(SearchText));

        public ICommand OpenConfigCommand => new RelayCommand(OpenConfig);

        private async Task Search()
        {
            var branchNames = await _searchService.GetBranchesWithText(SearchText);

            var branches = new ObservableCollection<BranchViewModel>();
            foreach (var branchName in branchNames)
            {
                branches.Add(new BranchViewModel(_appSettingOptions)
                {
                    Name = branchName
                });
            }

            Branches = branches;
        }

        private void OpenConfig()
        {
            MessengerInstance.Send<ConfigDialogRequest>(null);
        }
    }
}
