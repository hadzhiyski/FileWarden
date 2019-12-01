
using System.IO;

namespace FileWarden.Core.Rename
{
    public class RenameWardenOptions : IWardenBaseOptions, IAppendFileNameWardenOptions
    {
        public RenameWardenOptions(string source, SearchOption search, string suffix, string prefix, string backup, bool noBackup, bool noCleanup, bool overwriteExistingFiles)
        {
            Source = source;
            Search = search;
            Suffix = suffix;
            Prefix = prefix;
            Backup = backup;
            NoBackup = noBackup;
            NoCleanup = noCleanup;
            OverwriteExistingFiles = overwriteExistingFiles;
        }

        public string Source { get; }
        public SearchOption Search { get; }
        public string Suffix { get; }
        public string Prefix { get; }
        public string Backup { get; }
        public bool NoBackup { get; set; }
        public bool NoCleanup { get; }
        public bool OverwriteExistingFiles { get; set; }
    }
}
