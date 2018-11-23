using reposer.Config;

namespace reposer.Rendering.Remd {
    public class RemdRendererFactory : IRendererFactory {
        public RemdRendererFactory (ConfigService configService) {
            this.configService = configService;
        }

        private readonly ConfigService configService;

        public IRenderer Create () {
            return new RemdRenderer (configService.WebsiteRepositoryPath, configService.RenderPath);
        }
    }
}