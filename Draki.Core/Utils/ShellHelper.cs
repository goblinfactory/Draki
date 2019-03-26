using System;
using System.Diagnostics;

namespace Draki
{
    public interface ILog
    {
        void Info(string text);
        void Error(string text);
    }

    public class ShellHelper
    {
        public static ILog Log = null;

        private readonly string _dir;

        public ShellHelper(string dir)
        {
            _dir = dir;
            Log.Info($"DosShell created, working directory :'{dir}'.");
        }

        public string Shell(string cmd)
        {
            Log.Info($"Shell '{cmd}'");
            var arguments = string.Format("/c {0}", cmd);
            var info = new ProcessStartInfo("cmd", arguments)
            {
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                WorkingDirectory = _dir
            };
            using (var process = new Process() { StartInfo = info })
            {
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();

                var output = process.StandardOutput.ReadToEnd();
                var errorOut = process.StandardError.ReadToEnd();
                process.WaitForExit();
                process.Dispose();
                if (!string.IsNullOrWhiteSpace(errorOut))
                {
                    Log.Error(errorOut);
                    throw new Exception(
                        string.Format("error during script execution. \n-----\nThe following is the output before error\n{0}\n--------\nError:\n{1}"
                        , output, errorOut)
                    );
                }
                return output;
            }
        }
    }
}

