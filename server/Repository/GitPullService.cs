using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Diagnostics;
using System.IO;
using reposer.Config;

namespace reposer.Repository
{
    public class GitPullService : RepositoryPullServiceBase
    {
        public GitPullService(ConfigService configService) : base(configService)
        { }

        private string lastPullOutput = "";

        protected override void OnPull()
        {
            Console.WriteLine("Git pull:");

            var pullOutput = RunShell("git", "pull");
            Console.WriteLine(pullOutput);

            if (pullOutput != lastPullOutput)
            {
                Console.WriteLine("Repo changed");
                InvokeRepositoryChanged();
                lastPullOutput = pullOutput;
            }
        }

        private string RunShell(string command, string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            Process p = new Process();

            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;

            startInfo.UseShellExecute = false;
            startInfo.Arguments = arguments;
            startInfo.FileName = command;
            startInfo.WorkingDirectory = repositoryPath;

            p.StartInfo = startInfo;
            p.Start();

            return p.StandardOutput.ReadToEnd();
        }
    }
}
