using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GitBranch.Services
{
    public interface ISearchService
    {
        Task<List<string>> GetBranchesWithText(string text);
    }
}
