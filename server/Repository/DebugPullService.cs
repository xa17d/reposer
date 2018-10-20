using reposer.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace reposer.Repository
{
    public class DebugPullService : RepositoryPullServiceBase
    {
        public DebugPullService(ConfigService configService) : base(configService)
        { }

        protected override void OnPull()
        {
            InvokeRepositoryChanged();
        }
    }
}
