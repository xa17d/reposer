using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reposer.Repository
{
    public class RepositoryChangedEventArgs : EventArgs
    {
        public RepositoryChangedEventArgs(string repositoryPath)
        {
            this.RepositoryPath = repositoryPath;
        }

        public string RepositoryPath { get; private set; }
    }
}
