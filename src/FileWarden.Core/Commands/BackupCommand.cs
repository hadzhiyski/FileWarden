using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace FileWarden.Core.Commands
{
    public class BackupCommand : IBackupCommand
    {
        private readonly IFileSystem _fs;
        private readonly IDirectoryInfo _rootBackupDirectory;

        public BackupCommand(IFileSystem fs)
        {
            _fs = fs;
            _rootBackupDirectory = _fs.DirectoryInfo.FromDirectoryName(Path.Join(Path.GetTempPath(), "warden"));
        }

        public void Cleanup()
        {
            if (_rootBackupDirectory.Exists)
            {
                _rootBackupDirectory.Delete(true);
            }
        }

        public void Create(string source, bool recursive)
        {
            Cleanup();

            _rootBackupDirectory.Create();

            var searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            var directory = _fs.DirectoryInfo.FromDirectoryName(source);

            if (!directory.Exists)
            {
                throw new DirectoryNotFoundException($"Directory '{source}' not found");
            }

            var filesToBackup = _fs.DirectoryInfo
                .FromDirectoryName(source)
                .EnumerateFiles("*", searchOption)
                .ToList();

            foreach (var file in filesToBackup)
            {
                var fileBackupDirectoryInfo = _fs.DirectoryInfo.FromDirectoryName(file.DirectoryName.Replace(source, _rootBackupDirectory.FullName));
                if (!fileBackupDirectoryInfo.Exists)
                {
                    fileBackupDirectoryInfo.Create();
                }
                var backupFilePath = Path.Join(fileBackupDirectoryInfo.FullName, file.Name);

                _fs.File.Copy(file.FullName, backupFilePath);
            }
        }

        public void Restore(string source, bool recursive)
        {
            var searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            var filesToRestore = _fs.DirectoryInfo
               .FromDirectoryName(_rootBackupDirectory.FullName)
               .EnumerateFiles("*", searchOption)
               .ToList();

            foreach (var file in filesToRestore)
            {
                var fileDirectoryInfo = _fs.DirectoryInfo.FromDirectoryName(file.DirectoryName.Replace(_rootBackupDirectory.FullName, source));
                if (!fileDirectoryInfo.Exists)
                {
                    fileDirectoryInfo.Create();
                }
                var restoreFilePath = Path.Join(fileDirectoryInfo.FullName, file.Name);

                _fs.File.Copy(file.FullName, restoreFilePath, true);
            }
        }
    }
}
