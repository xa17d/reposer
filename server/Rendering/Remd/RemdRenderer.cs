using System.IO;

namespace reposer.Rendering.Remd {
    public class RemdRenderer : IRenderer {
        public RemdRenderer (string sourcePath, string destinationPath) {
            this.renderSource = new DirectoryInfo (sourcePath);
            this.renderDestination = new DirectoryInfo (destinationPath);
        }

        private readonly DirectoryInfo renderSource;
        private readonly DirectoryInfo renderDestination;

        public void RenderFilesRecursively (DirectoryInfo source, DirectoryInfo target) {
            foreach (DirectoryInfo dir in source.GetDirectories ()) {
                if (!dir.Name.StartsWith (".")) {
                    RenderFilesRecursively (dir, target.CreateSubdirectory (dir.Name));
                }
            }
            foreach (FileInfo file in source.GetFiles ("*.md")) {
                new RemdCompiler (file.FullName, Path.ChangeExtension (Path.Combine (target.FullName, file.Name), ".html")).Compile ();
            }
        }

        public void Render () {
            RenderFilesRecursively (renderSource, renderDestination);
        }
    }
}