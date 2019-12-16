namespace FileWarden.Core.Backup
{
    public interface IBackupWarden : IWarden<IBackupWardenOptions>
    {
        void Cleanup(IBackupWardenOptions opts);
    }
}
