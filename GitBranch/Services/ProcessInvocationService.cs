using GitBranch.Common;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GitBranch.Services
{
    public class ProcessInvocationService : IProcessInvocationService
    {
        private AppSettings _appSettings;
        
        public ProcessInvocationService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public Task<ProcessInvocationResult> Invoke(ProcessInvocationRequest request)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo(request.FileName, request.Arguments)
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WorkingDirectory = _appSettings.SourceDirectory
                },
                EnableRaisingEvents = true,
                
            };

            var taskCompletionSource = new TaskCompletionSource<ProcessInvocationResult>();
            var result = new ProcessInvocationResult();
            process.OutputDataReceived += (sender, args) =>
            {
                var data = args.Data;
                if (data != null)
                {
                    result.OutputLines.Add(args.Data);
                }
            };

            process.ErrorDataReceived += (sender, args) =>
            {
                result.ErrorLines.Add(args.Data);
            };

            process.Exited += (sender, args) =>
            {
                taskCompletionSource.SetResult(result);
            };

            process.Start();
            process.BeginOutputReadLine();

            return taskCompletionSource.Task;
        }
    }
}
