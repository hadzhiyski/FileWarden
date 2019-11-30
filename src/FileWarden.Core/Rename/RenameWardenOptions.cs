using FileWarden.Core.Rename.Suffix;

using System.IO;

namespace FileWarden.Core.Rename
{
    public class RenameWardenOptions : IWardenBaseOptions, IAppendSuffixWardenOptions
    {
        public RenameWardenOptions(string source, SearchOption search, string suffix, bool createBackup, bool noCleanup)
        {
            Source = source;
            Search = search;
            Suffix = suffix;
            CreateBackup = createBackup;
            NoCleanup = noCleanup;
        }

        public string Source { get; }
        public SearchOption Search { get; }
        public string Suffix { get; }
        public bool CreateBackup { get; }
        public bool NoCleanup { get; }
    }
}
