namespace FileWarden.Core.Backup
{
    public interface IBackupWarden : IWarden<IWardenBaseOptions>
    {
        void Create(IWardenBaseOptions opts);

        void Restore(IWardenBaseOptions opts);

        void Cleanup();
    }
}
