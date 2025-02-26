using GitImporter;
using GitImporter.Models;

using SvnDumpParser.Models;

namespace SvnDump2Git
{
    public static class DumpToGitModelConverter
    {
        public static GitRevision ConvertRevision(DumpRevision dumpRevision)
        {
            var gitRevision = new GitRevision
            {
                Author = dumpRevision.Author,
                Date = dumpRevision.Date,
                LogMessage = dumpRevision.Comment,
                Number = dumpRevision.Number,
            };

            // Copy properties
            foreach (var prop in dumpRevision.Properties)
            {
                gitRevision.Properties[prop.Key] = prop.Value;
            }

            // Convert and add each DumpNode to GitNodeChange
            foreach (var dumpNode in dumpRevision.Changes)
            {
                gitRevision.AddNode(ConvertNode(dumpNode));
            }

            return gitRevision;
        }

        private static GitNodeChange ConvertNode(DumpNode dumpNode)
        {
            var gitNodeChange = new GitNodeChange
            {
                Action = ConvertChangeAction(dumpNode.NodeAction),
                Kind = ConvertNodeKind(dumpNode.NodeKind),
                Path = dumpNode.NodePath,
                BinaryContent = dumpNode.ContentInfo.BinaryContent,
                TextContent = dumpNode.ContentInfo.TextContent,
                CopyFromPath = dumpNode.CopyInfo?.CopyFromPath,
                CopyFromRevision = dumpNode.CopyInfo?.CopyFromRevision,
            };

            // Copy properties
            foreach (var prop in dumpNode.Properties)
            {
                gitNodeChange.Properties[prop.Key] = prop.Value;
            }

            return gitNodeChange;
        }

        private static EChangeAction ConvertChangeAction(EDumpChangeAction dumpAction)
        {
            return dumpAction switch
            {
                EDumpChangeAction.Add => EChangeAction.Add,
                EDumpChangeAction.Delete => EChangeAction.Delete,
                EDumpChangeAction.Modify => EChangeAction.Modify,
                EDumpChangeAction.Replace => EChangeAction.Replace,
                _ => EChangeAction.None, // Default case, or handle specific unknown cases if needed
            };
        }

        private static ENodeKind ConvertNodeKind(EDumpNodeKind dumpKind)
        {
            return dumpKind switch
            {
                EDumpNodeKind.File => ENodeKind.File,
                EDumpNodeKind.Directory => ENodeKind.Directory,
                EDumpNodeKind.Unknown => ENodeKind.Unknown,
                EDumpNodeKind.SymbolicLink => ENodeKind.SymbolicLink,
                _ => ENodeKind.None, // Default case, or handle specific unknown cases if needed
            };
        }
    }
}