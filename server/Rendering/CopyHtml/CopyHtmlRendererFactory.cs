using reposer.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reposer.Rendering.CopyHtml
{
    public class CopyHtmlRendererFactory : IRendererFactory
    {
        public CopyHtmlRendererFactory(ConfigService configService)
        {
            this.configService = configService;
        }

        private readonly ConfigService configService;

        public IRenderer Create()
        {
            return new CopyHtmlRenderer(configService.WebsiteRepositoryPath, configService.RenderPath);
        }
    }
}
