namespace FileWarden.Core.Backup
{
    public interface IBackupWarden : IWarden<IBackupWardenOptions>
    {
        void Create(IBackupWardenOptions opts);

        void Restore(IBackupWardenOptions opts);

        void Cleanup(IBackupWardenOptions opts);
    }
}
