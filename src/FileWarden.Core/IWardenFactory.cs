namespace FileWarden.Core
{
    public interface IWardenFactory
    {
        TWarden Resolve<TWarden, TOptions>()
            where TWarden : IWarden<TOptions>
            where TOptions : IWardenBaseOptions;
    }
}
