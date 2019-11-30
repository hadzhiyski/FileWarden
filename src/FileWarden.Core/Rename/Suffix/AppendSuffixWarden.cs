using System.Collections.Generic;
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

            var overwrittenFiles = new List<string>();

            foreach (var file in files)
            {
                if (overwrittenFiles.Contains(file.FullName))
                {
                    continue;
                }

                var fileDirectory = file.DirectoryName;
                var fileNameWithoutExtension = _fs.Path.GetFileNameWithoutExtension(file.Name);
                var fileExtension = _fs.Path.GetExtension(file.Name);

                var fileNameWithSuffix = $"{fileNameWithoutExtension}{options.Suffix}{fileExtension}";

                var fileNameWithSuffixPath = _fs.Path.Combine(fileDirectory, fileNameWithSuffix);
                var fileNameWithSuffixInfo = _fs.FileInfo.FromFileName(fileNameWithSuffixPath);

                if (options.OverwriteExistingFiles && fileNameWithSuffixInfo.Exists)
                {
                    overwrittenFiles.Add(fileNameWithSuffixPath);
                    fileNameWithSuffixInfo.Delete();
                }

                file.MoveTo(fileNameWithSuffixPath);
            }
        }
    }
}
