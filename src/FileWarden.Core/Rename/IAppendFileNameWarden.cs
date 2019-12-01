namespace FileWarden.Core.Rename
{
    public interface IAppendFileNameWarden : IWarden<IAppendFileNameWardenOptions>
    {
        bool CanExecute(RenameWardenOptions options);
    }
}