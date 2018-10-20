using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace reposer.Rendering.CopyHtml
{
    public class CopyHtmlRenderer : IRenderer
    {
        public CopyHtmlRenderer(string sourcePath, string destinationPath)
        {
            this.renderSource = new DirectoryInfo(sourcePath);
            this.renderDestination = new DirectoryInfo(destinationPath);
        }

        private readonly DirectoryInfo renderSource;
        private readonly DirectoryInfo renderDestination;

        public static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target, string pattern)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
            {
                if (!dir.Name.StartsWith("."))
                {
                    CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name), pattern);
                }
            }
            foreach (FileInfo file in source.GetFiles(pattern))
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
        }

        public void Render()
        {
            CopyFilesRecursively(renderSource, renderDestination, "*.html");
        }
    }
}
