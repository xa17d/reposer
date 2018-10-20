using reposer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reposer.Rendering
{
    public class RenderService
    {
        public RenderService(IRepositoryPullService gitPullService, IRendererFactory rendererFactory)
        {
            this.gitPullService = gitPullService;
            this.gitPullService.RepositoryChanged += RepositoryChanged;
        }

        private readonly IRepositoryPullService gitPullService;
        private readonly IRendererFactory rendererFactory;

        private void RepositoryChanged(object sender, RepositoryChangedEventArgs e)
        {
            var renderer = rendererFactory.Create();
            renderer.Render();
        }
    }
}
