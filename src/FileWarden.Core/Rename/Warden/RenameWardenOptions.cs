namespace FileWarden.Core.Rename.Warden
{
    public class RenameWardenOptions
    {
        public string Source { get; set; }
        public string Suffix { get; set; }
        public bool Recursive { get; set; }
        public bool CreateBackup { get; set; }
    }
}
