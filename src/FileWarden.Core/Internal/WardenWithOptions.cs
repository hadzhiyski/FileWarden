namespace FileWarden.Core.Internal
{
    internal sealed class WardenWithOptions<TWarden, TOptions> : IWardenWithOptions
        where TWarden : IWarden<TOptions>
        where TOptions : IWardenBaseOptions
    {
        private readonly TWarden _warden;
        private readonly TOptions _options;

        public WardenWithOptions(TWarden warden, TOptions options)
        {
            _warden = warden;
            _options = options;
        }

        public void Execute() => _warden.Execute(_options);

        public void Rollback() => _warden.Rollback(_options);
    }
}
