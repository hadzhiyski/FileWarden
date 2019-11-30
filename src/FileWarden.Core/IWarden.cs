namespace FileWarden.Core
{
    public interface IWarden<TOptions>
        where TOptions : notnull
    {
        void Execute(TOptions options);
    }
}