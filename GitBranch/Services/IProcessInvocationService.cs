using System.Threading.Tasks;

namespace GitBranch.Services
{
    public interface IProcessInvocationService
    {
        Task<ProcessInvocationResult> Invoke(ProcessInvocationRequest request);
    }
}
