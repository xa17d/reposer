using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace reposer.Rendering.Remd {
    public class RemdCompiler {
        public RemdCompiler (string sourcePath, string destinationPath) {
            this.sourcePath = sourcePath;
            this.destinationPath = destinationPath;
        }

        private string sourcePath;
        private string destinationPath;

        private static Regex regexHeader3 = new Regex ("^### (.*)$");
        private static Regex regexHeader2 = new Regex ("^## (.*)$");
        private static Regex regexHeader1 = new Regex ("^# (.*)$");
        private static Regex inlineCode = new Regex ("`(.*)`");

        private StringBuilder html = new StringBuilder ();
        public void Compile () {
            var lines = File.ReadAllLines (sourcePath);

            foreach (var l in lines) {
                var handled =
                    Header (regexHeader3, "h3", l) ||
                    Header (regexHeader2, "h2", l) ||
                    Header (regexHeader1, "h1", l) ||
                    Paragraph (l);
            }

            File.WriteAllText (destinationPath, html.ToString ());
        }

        private bool Header (Regex regex, string tag, string line) {
            var matches = regex.Matches (line);
            var match = matches.FirstOrDefault ();
            if (match != null) {
                html.AppendLine ();
                html.Append ($"<{tag}>");
                Inline (match.Groups[1].Value);
                html.Append ($"</{tag}>");

                return true;
            }
            return false;
        }

        private bool Inline (string code) {
            html.Append (WebUtility.HtmlEncode (code));
            return true;
        }

        private bool Paragraph (string code) {
            html.Append ("<p>");
            Inline (code);
            html.Append ("</p>");
            return true;
        }
    }
}