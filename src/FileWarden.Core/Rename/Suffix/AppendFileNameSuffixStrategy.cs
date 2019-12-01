namespace FileWarden.Core.Rename.Suffix
{
    public class AppendFileNameSuffixStrategy : IAppendFileNameStrategy
    {
        public bool CanExecute(RenameWardenOptions options) =>
            !string.IsNullOrWhiteSpace(options.Suffix);

        public string FormatFileName(string fileName, string extension, IAppendFileNameWardenOptions options) =>
            $"{fileName}{options.Suffix}{extension}";
    }
}
