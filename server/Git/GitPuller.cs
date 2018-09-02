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

namespace reposer.Git
{
    public class GitPuller
    {
        public void Start() {
            workerThread = new Thread(Work);
            workerThread.IsBackground = true;
            workerThread.Start();
        }

        private Thread workerThread;

        private string lastPullOutput = "";
        private void Work() {
            while (true) {
                try {
                    Console.WriteLine("Git pull:");

                    var pullOutput = RunShell("git", "pull");
                    Console.WriteLine(pullOutput);

                    if (pullOutput != lastPullOutput) 
                    {
                        Console.WriteLine("Repo changed, copying...");
                        CopyFilesRecursively(
                            new DirectoryInfo("/app/repository"), 
                            new DirectoryInfo("/app/webroot")
                        );
                        Console.WriteLine("copy done.");
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

        private string RunShell(string command, string arguments) {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            Process p = new Process();

            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;

            startInfo.UseShellExecute = false;
            startInfo.Arguments = arguments;
            startInfo.FileName = command;
            startInfo.WorkingDirectory = "/app/repository/";

            p.StartInfo = startInfo;
            p.Start();

            return p.StandardOutput.ReadToEnd(); 
        }

        public static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target) {
            foreach (DirectoryInfo dir in source.GetDirectories()) 
            {
                if (!dir.Name.StartsWith(".")) 
                {
                    CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
                }
            }
            foreach (FileInfo file in source.GetFiles("*.html"))
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
        }
    }
}
