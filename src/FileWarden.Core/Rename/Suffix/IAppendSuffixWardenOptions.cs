namespace FileWarden.Core.Rename.Suffix
{
    public interface IAppendSuffixWardenOptions : IWardenBaseOptions
    {
        string Suffix { get; }
    }
}