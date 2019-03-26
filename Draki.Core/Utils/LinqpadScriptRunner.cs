using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Draki.Utils
{
    public class LinqpadScriptRunner
    {
        private readonly string _lprunDir;
        private readonly string _scriptDir;
        private readonly ILog _log;

        /// <param name="pathToLPRun">the directly only of where Linqpad LPRun is located</param>
        /// <param name="scriptFolder">the folder where the linqpad scripts</param>
        /// <param name="log"></param>
        public LinqpadScriptRunner(string lprunDir, string scriptDir, ILog log = null)
        {
            _scriptDir = scriptDir;
            _lprunDir = lprunDir;
            _log = log;
        }

        public string RunScript(string buildScriptAndArgs)
        {
            _log.Info($"Running script :{buildScriptAndArgs}");
            var shell = new ShellHelper(_scriptDir);
            var output = shell.Shell(@"""lprun " + buildScriptAndArgs + "\"");
            Console.WriteLine(output);
            return output;
        }

        public string RunScript(string buildScript, params string[] args)
        {
            var buildScriptAndArgs = buildScript + " " + string.Join(" ", args);
            var output = RunScript(buildScriptAndArgs);
            return output;
        }
    }
}
