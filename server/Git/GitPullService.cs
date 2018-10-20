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

namespace reposer.Git
{
    public class GitPullService : IGitPullService
    {
        public GitPullService(ConfigService configService)
        {
            this.repositoryPath = configService.WebsiteRepositoryPath;
            Start();
        }

        private readonly string repositoryPath;

        private void Start()
        {
            workerThread = new Thread(Work)
            {
                IsBackground = true
            };
            workerThread.Start();
        }

        public event EventHandler<RepositoryChangedEventArgs> RepositoryChanged;

        private void InvokeRepositoryChanged()
        {
            RepositoryChanged?.Invoke(this, new RepositoryChangedEventArgs(repositoryPath));
        }

        private Thread workerThread;

        private string lastPullOutput = "";
        private void Work()
        {
            while (true)
            {
                try
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
                catch (Exception e)
                {
                    Console.WriteLine($"Exception: {e.Message}");
                }

                Thread.Sleep(30000);
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
