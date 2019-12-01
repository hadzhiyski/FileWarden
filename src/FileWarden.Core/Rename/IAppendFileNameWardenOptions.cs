namespace FileWarden.Core.Rename
{
    public interface IAppendFileNameWardenOptions : IWardenBaseOptions
    {
        string Suffix { get; }
        string Prefix { get; }
        bool OverwriteExistingFiles { get; }
    }
}