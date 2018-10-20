using reposer.Config;
using reposer.Rendering.CopyHtml;
using reposer.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace reposer.Rendering
{
    public class RenderService
    {
        public RenderService(
            ConfigService configService,
            IRepositoryPullService pullService,
            IRendererFactory rendererFactory)
        {
            this.configService = configService;
            this.pullService = pullService;
            this.rendererFactory = rendererFactory;

            this.pullService.RepositoryChanged += RepositoryChanged;
        }

        private readonly ConfigService configService;
        private readonly IRepositoryPullService pullService;
        private readonly IRendererFactory rendererFactory;

        private void RepositoryChanged(object sender, RepositoryChangedEventArgs e)
        {
            var renderer = rendererFactory.Create();
            renderer.Render();

            SwapRenderOutputToWebroot();
        }

        private void SwapRenderOutputToWebroot()
        {
            var swapPath = configService.WebrootSwapPath;
            var webrootPath = configService.WebrootPath;
            var renderPath = configService.RenderPath;

            CopyHtmlRenderer.CopyFilesRecursively(
                new DirectoryInfo(renderPath),
                new DirectoryInfo(webrootPath),
                "*"
            );
        }
    }
}