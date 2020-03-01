using GitBranch.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitBranch.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel
            => App.ServiceProvider.GetRequiredService<MainViewModel>();
        public ConfigDialogViewModel ConfigDialogViewModel
            => App.ServiceProvider.GetRequiredService<ConfigDialogViewModel>();
    }
}
