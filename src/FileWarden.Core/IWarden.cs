namespace FileWarden.Core
{
    public interface IWarden<in TOptions>
        where TOptions : IWardenBaseOptions
    {
        void Execute(TOptions options);
        void Rollback(TOptions options);
    }
}