using FileWarden.Core.Rename.Suffix;

using System.IO;

namespace FileWarden.Core.Rename
{
    public class RenameWardenOptions : IWardenBaseOptions, IAppendFileNameWardenOptions
    {
        public RenameWardenOptions(string source, SearchOption search, string suffix, string prefix, bool createBackup, bool noCleanup, bool overwriteExistingFiles)
        {
            Source = source;
            Search = search;
            Suffix = suffix;
            Prefix = prefix;
            CreateBackup = createBackup;
            NoCleanup = noCleanup;
            OverwriteExistingFiles = overwriteExistingFiles;
        }

        public string Source { get; }
        public SearchOption Search { get; }
        public string Suffix { get; }
        public string Prefix { get; }
        public bool CreateBackup { get; }
        public bool NoCleanup { get; }
        public bool OverwriteExistingFiles { get; set; }
    }
}
