using FileWarden.Core.Backup;

namespace FileWarden.Core.Rename
{
    public class RenameWardenOptions : BackupWardenOptions
    {
        public string Suffix { get; set; }
        public bool CreateBackup { get; set; }
        public bool NoCleanup { get; set; }
    }
}
