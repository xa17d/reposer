using reposer.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace reposer.Repository
{
    public abstract class RepositoryPullServiceBase : IRepositoryPullService
    {
        public RepositoryPullServiceBase(ConfigService configService)
        {
            this.repositoryPath = configService.WebsiteRepositoryPath;
            Start();
        }

        protected readonly string repositoryPath;
        private readonly TimeSpan interval;

        private void Start()
        {
            workerThread = new Thread(Work)
            {
                IsBackground = true
            };
            workerThread.Start();
        }

        public event EventHandler<RepositoryChangedEventArgs> RepositoryChanged;

        protected void InvokeRepositoryChanged()
        {
            RepositoryChanged?.Invoke(this, new RepositoryChangedEventArgs(repositoryPath));
        }

        private Thread workerThread;

        private void Work()
        {
            while (true)
            {
                try
                {
                    OnPull();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception: {e.Message}");
                }

                Thread.Sleep((int)interval.TotalSeconds);
            }
        }

        protected abstract void OnPull();
    }
}
