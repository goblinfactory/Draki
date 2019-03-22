using System.Diagnostics;

namespace Draki.Tests.Internal
{
    public class ProcessRunner
    {
        public static Process StartProcess(string cmd, string args, string directory)
        {
            ProcessStartInfo info = new ProcessStartInfo(cmd, args);
            info.RedirectStandardError = true;
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            info.WorkingDirectory = directory;
            var process = Process.Start(info);
            return process;
        }
    }
}
