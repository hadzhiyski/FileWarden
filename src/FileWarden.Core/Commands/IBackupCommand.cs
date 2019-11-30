namespace FileWarden.Core.Commands
{
    public interface IBackupCommand
    {
        void Create(string source, bool recursive);
        void Restore(string source, bool recursive);
        void Cleanup();
    }
}
