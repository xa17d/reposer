using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reposer.Git
{
    interface IGitPullService
    {
        event EventHandler<RepositoryChangedEventArgs> RepositoryChanged;
    }
}
