using GitImporter.Interfaces;

namespace SvnDumpParser.Models
{
    public class CommandLineOptions
    {
        public string DumpFilePath { get; set; }
        public bool ConvertToGit { get; set; }
        public string GitRepoPath { get; set; }
        public ParserOptions ParserOptions { get; set; } = new ParserOptions();
        public bool ShowHelp { get; set; }

        public bool FullInMemory { get; set; }

        public IAuthorsMap? AuthorsMap { get; set; } = null;
    }
}
