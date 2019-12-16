
using FileWarden.Core.Rename.Prefix;
using FileWarden.Core.Rename.Suffix;

namespace FileWarden.Core.Rename
{
    public class RenameWarden : IRenameWarden
    {
        private readonly IAppendSuffixWarden _suffixWarden;
        private readonly IAppendPrefixWarden _prefixWarden;

        public RenameWarden(IAppendSuffixWarden suffixWarden, IAppendPrefixWarden prefixWarden)
        {
            _suffixWarden = suffixWarden;
            _prefixWarden = prefixWarden;
        }

        public void Execute(RenameWardenOptions options)
        {
            _prefixWarden.Execute(options);
            _suffixWarden.Execute(options);
        }

        public void Rollback(RenameWardenOptions options)
        {
            _prefixWarden.Rollback(options);
            _suffixWarden.Rollback(options);
        }
    }
}
