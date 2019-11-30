using System.IO.Abstractions;
using System.Linq;

namespace FileWarden.Core.Rename.Suffix
{
    public class AppendSuffixWarden : IAppendSuffixWarden
    {
        private readonly IFileSystem _fs;

        public AppendSuffixWarden(IFileSystem fs)
        {
            _fs = fs;
        }

        public void Execute(IAppendSuffixWardenOptions options)
        {
            var files = _fs.DirectoryInfo
                .FromDirectoryName(options.Source)
                .EnumerateFiles("*", options.Search)
                .ToList();

            foreach (var file in files)
            {
                var fileDirectory = file.DirectoryName;
                var fileNameWithoutExtension = _fs.Path.GetFileNameWithoutExtension(file.Name);
                var fileExtension = _fs.Path.GetExtension(file.Name);

                var fileNameWithSuffix = $"{fileNameWithoutExtension}{options.Suffix}{fileExtension}";

                var fileNameWithSuffixPath = _fs.Path.Combine(fileDirectory, fileNameWithSuffix);

                file.MoveTo(fileNameWithSuffixPath);
            }
        }
    }
}
