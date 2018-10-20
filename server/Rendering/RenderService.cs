using reposer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reposer.Rendering
{
    public class RenderService
    {
        public RenderService(IRepositoryPullService pullService, IRendererFactory rendererFactory)
        {
            this.pullService = pullService;
            this.rendererFactory = rendererFactory;

            this.pullService.RepositoryChanged += RepositoryChanged;
        }

        private readonly IRepositoryPullService pullService;
        private readonly IRendererFactory rendererFactory;

        private void RepositoryChanged(object sender, RepositoryChangedEventArgs e)
        {
            var renderer = rendererFactory.Create();
            renderer.Render();
        }
    }
}
