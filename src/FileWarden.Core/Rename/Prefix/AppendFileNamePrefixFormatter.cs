namespace FileWarden.Core.Rename.Prefix
{
    public class AppendFileNamePrefixFormatter : AppendFileNameBaseFormatter, IAppendFileNameStrategy
    {
        public bool CanExecute(RenameWardenOptions options) =>
            !string.IsNullOrWhiteSpace(options.Prefix);

        public string FormatFileName(string fileName, string extension, IAppendFileNameWardenOptions options) =>
            $"{options.Prefix}{fileName}{extension}";
    }
}
