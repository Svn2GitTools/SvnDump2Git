using SvnDumpParser;
using SvnDumpParser.Interfaces;
using SvnDumpParser.Models;

namespace SvnDumpParser.Services
{
    public class CommandLineParserService : ICommandLineParser
    {
        public CommandLineOptions Parse(string[] args)
        {
            CommandLineOptions options = new CommandLineOptions();

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-v")
                {
                    options.ParserOptions.Verbose = true;
                }
                else if (args[i] == "--git")
                {
                    options.ConvertToGit = true;
                    if (i + 1 < args.Length)
                    {
                        options.GitRepoPath = args[i + 1];
                        i++; // consume git repo path
                    }
                    else
                    {
                        Console.WriteLine("Error: --git requires a git repository path");
                        options.ShowHelp = true;
                        return options;
                    }
                }
                else if (args[i] == "--full-inmemory")
                {
                    options.FullInMemory = true;
                }
                else if (options.DumpFilePath == null) // Only set the file path once
                {
                    options.DumpFilePath = args[i];
                }
                else
                {
                    Console.WriteLine("Usage: SvnDumpParser [-v] <dump_file> [--git <git_repo_path>] [--full-inmemory]");
                    options.ShowHelp = true;
                    return options;
                }
            }

            if (options.DumpFilePath == null)
            {
                Console.WriteLine("Usage: SvnDumpParser [-v] <dump_file> [--git <git_repo_path>] [--full-inmemory]");
                options.ShowHelp = true;
                return options;
            }

            return options;
        }
    }
}
