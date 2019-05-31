using System.Linq;
using System.Text.RegularExpressions;

namespace Frameio.NET.Parsers
{
    public class LinkHeaderParser
    {
        public string FirstLink { get; }

        public string PreviousLink { get; }

        public string NextLink { get; }

        public string LastLink { get; }

        public LinkHeaderParser(string linkHeaderStr)
        {
            if (string.IsNullOrWhiteSpace(linkHeaderStr))
            {
                return;
            }

            string[] linkStrings = linkHeaderStr.Split(',');

            if (!linkStrings.Any())
            {
                return;
            }

            foreach (string linkString in linkStrings)
            {
                var relMatch = Regex.Match(linkString, "(?<=rel=\").+?(?=\")", RegexOptions.IgnoreCase);
                var linkMatch = Regex.Match(linkString, "(?<=<).+?(?=>)", RegexOptions.IgnoreCase);

                if (!relMatch.Success || !linkMatch.Success)
                {
                    continue;
                }

                string rel = relMatch.Value.ToUpper();
                string link = linkMatch.Value;

                switch (rel)
                {
                    case "FIRST":
                        FirstLink = link;
                        break;
                    case "PREV":
                        PreviousLink = link;
                        break;
                    case "NEXT":
                        NextLink = link;
                        break;
                    case "LAST":
                        LastLink = link;
                        break;
                }
            }
        }
    }
}
