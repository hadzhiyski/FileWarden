namespace FileWarden.Core.Rename.Prefix
{
    public interface IAppendPrefixWardenOptions : IWardenBaseOptions
    {
        string Prefix { get; }
    }
}
