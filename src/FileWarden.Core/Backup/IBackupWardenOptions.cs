namespace FileWarden.Core.Backup
{
    public interface IBackupWardenOptions : IWardenBaseOptions
    {
        string Backup { get; }

        bool NoBackup { get; }

        bool NoCleanup { get; }
    }
}
