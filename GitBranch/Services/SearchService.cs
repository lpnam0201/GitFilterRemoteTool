using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using GitBranch.Common;
using Microsoft.Extensions.Options;

namespace GitBranch.Services
{
    public class SearchService : ISearchService
    {
        private IProcessInvocationService _processInvocationService;
        private AppSettings _appSettings;

        public SearchService(IProcessInvocationService processInvocationService, IOptions<AppSettings> appSettings)
        {
            _processInvocationService = processInvocationService;
            _appSettings = appSettings.Value;
        }

        public async Task<List<string>> GetBranchesWithText(string text)
        {
            var request = new ProcessInvocationRequest
            {
                FileName = "git",
                Arguments = "branch --all"
            };

            if (_appSettings.CallGitFetchBeforeSearch)
            {
                await PerformGitFetch();
            }

            return await _processInvocationService.Invoke(request)
                .ContinueWith(t => t.Result.OutputLines
                    .Select(line => line.TrimStart('*').Trim())
                    .Where(line => line.Contains(text, StringComparison.OrdinalIgnoreCase))
                    .ToList());
        }

        private Task<ProcessInvocationResult> PerformGitFetch()
        {
            return _processInvocationService.Invoke(new ProcessInvocationRequest
            {
                FileName = "git",
                Arguments = "fetch"
            });
        }
    }
}
