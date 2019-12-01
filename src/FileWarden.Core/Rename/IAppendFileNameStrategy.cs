namespace FileWarden.Core.Rename
{
    public interface IAppendFileNameStrategy
    {
        string FormatFileName(string fileName, string extension, IAppendFileNameWardenOptions options);
        bool CanExecute(RenameWardenOptions options);
    }
}
