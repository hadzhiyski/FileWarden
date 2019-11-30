using FileWarden.Core.Rename.Warden;

namespace FileWarden.Core.Rename.Builder
{
    public class RenameWardenBuilder : IRenameWardenBuilder
    {
        private RenameWardenOptions _options = new RenameWardenOptions();

        public IWarden Build()
        {
            var warden = new RenameWarden(_options);

            Reset();

            return warden;
        }

        public IRenameWardenBuilder Recursive(bool recursive)
        {
            _options.Recursive = recursive;

            return this;
        }

        public void Reset()
        {
            _options = new RenameWardenOptions();
        }

        public IRenameWardenBuilder WithBackup(bool shouldCreateBackup)
        {
            _options.CreateBackup = shouldCreateBackup;

            return this;
        }

        public IRenameWardenBuilder WithSource(string sourcePath)
        {
            _options.Source = sourcePath;

            return this;
        }

        public IRenameWardenBuilder WithSuffix(string suffix)
        {
            _options.Suffix = suffix;

            return this;
        }
    }
}
