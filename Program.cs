using GitImporter;

using SvnDumpParser;
using SvnDumpParser.Interfaces;
using SvnDumpParser.Models;
using SvnDumpParser.Services;

namespace SvnDump2Git;

public class Program
{
    public static void Main(string[] args)
    {
        ICommandLineParser commandLineParser = new CommandLineParserService();
        ISvnDumpParser svnDumpParser = new SvnDumpParserService();
        //IGitConverter gitConverter = new GitConverterService();

        try
        {
            var options = commandLineParser.Parse(args);

            if (options.ShowHelp)
            {
                return;
            }

            if (options.FullInMemory)
            {
                FullMemoryConverter(svnDumpParser, options);
            }
            else
            {
                StepConverter(svnDumpParser, options);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private static void FullMemoryConverter(ISvnDumpParser svnDumpParser, CommandLineOptions options)
    {
        var parserActions = new FullMemoryParserActions();

        //List<Revision> revisions = 
        svnDumpParser.Parse(options.DumpFilePath, parserActions, options.ParserOptions);

        var revisions = parserActions.Revisions;
        if (options.ConvertToGit)
        {

            using (var compositionRoot = new CompositionRoot(options.GitRepoPath, options.AuthorsMap))
            {
                // IGitFullConverter fullConverter =
                //    compositionRoot.CreateFullConverter();
                //fullConverter.Convert(revisions);
                var revisionConverter = compositionRoot.GetRevisionConverter();

                foreach (var dumpRevision in revisions)
                {
                    var gitRevision = DumpToGitModelConverter.ConvertRevision(dumpRevision);

                    revisionConverter.ConvertRevision(
                        gitRevision); // Reuse the step-by-step conversion logic
                }

                compositionRoot.Checkout();
            }
        }
    }

    private static void StepConverter(ISvnDumpParser svnDumpParser, CommandLineOptions options)
    {
        using (var compositionRoot = new CompositionRoot(options.GitRepoPath, options.AuthorsMap))
        {
            // Create the revision converter once, to be used for each revision.
            var revisionConverter = compositionRoot.GetRevisionConverter();

            // Create the step‑based parser actions.
            var parserActions = new StepParserActions();

            if (options.ConvertToGit)
            {
                // When a revision is added during parsing, immediately convert it.
                parserActions.OnRevisionAdded = dumpRevision =>
                    {
                        var gitRevision = DumpToGitModelConverter.ConvertRevision(dumpRevision);
                        revisionConverter.ConvertRevision(gitRevision);
                    };

                // Optionally, you can also hook into node events here if needed.
                // For example:
                // parserActions.OnNodeAdded = change => { /* Process node addition step‐by‐step */ };
                // parserActions.OnNodeContentUpdated = change => { /* Process node content updates */ };

                // Once parsing is complete, perform a checkout.
                parserActions.OnParsingEnded = () => { compositionRoot.Checkout(); };
            }

            // Parse the dump file. The parser actions will trigger the delegates as the dump is processed.
            svnDumpParser.Parse(options.DumpFilePath, parserActions, options.ParserOptions);
        }
    }

}