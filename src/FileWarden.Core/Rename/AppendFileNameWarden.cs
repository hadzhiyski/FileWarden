using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace FileWarden.Core.Rename
{
    public class AppendFileNameWarden : IAppendFileNameWarden
    {
        private readonly IFileSystem _fs;
        private readonly IAppendFileNameStrategy _appendFileNameStrategy;

        public AppendFileNameWarden(IFileSystem fs, IAppendFileNameStrategy appendFileNameStrategy)
        {
            _fs = fs;
            _appendFileNameStrategy = appendFileNameStrategy;
        }

        public bool CanExecute(RenameWardenOptions options) =>
            _appendFileNameStrategy.CanExecute(options);

        public void Execute(IAppendFileNameWardenOptions options)
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

                var fileNameWithSuffix = _appendFileNameStrategy.FormatFileName(fileNameWithoutExtension, fileExtension, options);

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
