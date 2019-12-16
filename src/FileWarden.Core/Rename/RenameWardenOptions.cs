
using FileWarden.Core.Backup;
using FileWarden.Core.Rename.Prefix;
using FileWarden.Core.Rename.Suffix;

using System.IO;

namespace FileWarden.Core.Rename
{
    public class RenameWardenOptions : IWardenBaseOptions, IAppendPrefixWardenOptions, IAppendSuffixWardenOptions, IBackupWardenOptions
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
        public string Backup { get; private set; }
        public bool NoBackup { get; }
        public bool NoCleanup { get; }
        public bool OverwriteExistingFiles { get; }
        public void WithTransactionIdentifier(string rawId)
        {
            var cleanedUpIdChars = string.Concat(rawId.Split(Path.GetInvalidFileNameChars(), System.StringSplitOptions.RemoveEmptyEntries));

            Backup = Path.Join(Backup, new string(cleanedUpIdChars));
        }
    }
}
