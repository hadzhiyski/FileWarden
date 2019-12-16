using FileWarden.Core.Rename;

namespace FileWarden.Core
{
    public interface IWardenContext
    {
        void Rename(RenameWardenOptions opts);
    }
}
