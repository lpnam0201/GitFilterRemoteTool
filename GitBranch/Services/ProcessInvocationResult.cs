using System.Collections.Generic;
using System.Linq;

namespace GitBranch.Services
{
    public class ProcessInvocationResult
    {
        public ProcessInvocationResult()
        {
            OutputLines = new List<string>();
            ErrorLines = new List<string>();
        }
        public IList<string> OutputLines { get; set; }
        public IList<string> ErrorLines { get; set; }
        public bool HasError => ErrorLines.Any();
    }
}
