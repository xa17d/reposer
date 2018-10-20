using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reposer.Repository
{
    public interface IRepositoryPullService
    {
        event EventHandler<RepositoryChangedEventArgs> RepositoryChanged;
    }
}
