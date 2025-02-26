using SvnDumpParser.Models;

namespace SvnDumpParser.Interfaces;

public interface ICommandLineParser
{
    CommandLineOptions Parse(string[] args);
}