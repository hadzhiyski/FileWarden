using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace FileWarden.Core.Backup
{
    public class BackupWarden : IBackupWarden
    {
        private readonly IFileSystem _fs;
        public BackupWarden(IFileSystem fs)
        {
            _fs = fs;
        }

        public void Cleanup(IBackupWardenOptions opts)
        {
            if (opts.NoCleanup) return;

            var backupDirectoryPath = opts.Backup;

            if (GetRootBackupDirectory(backupDirectoryPath).Exists)
            {
                GetRootBackupDirectory(backupDirectoryPath).Delete(true);
            }
        }

        public void Execute(IBackupWardenOptions opts)
        {
            if (opts.NoBackup) return;

            Cleanup(opts);

            var backupDirectoryPath = opts.Backup;

            GetRootBackupDirectory(backupDirectoryPath).Create();

            var directory = _fs.DirectoryInfo.FromDirectoryName(opts.Source);

            if (!directory.Exists)
            {
                throw new DirectoryNotFoundException($"Directory '{opts.Source}' not found");
            }

            var filesToBackup = _fs.DirectoryInfo
                .FromDirectoryName(opts.Source)
                .EnumerateFiles("*", opts.Search)
                .ToList();

            foreach (var file in filesToBackup)
            {
                var fileBackupDirectoryInfo = _fs.DirectoryInfo.FromDirectoryName(file.DirectoryName.Replace(opts.Source, GetRootBackupDirectory(backupDirectoryPath).FullName));
                if (!fileBackupDirectoryInfo.Exists)
                {
                    fileBackupDirectoryInfo.Create();
                }
                var backupFilePath = _fs.Path.Combine(fileBackupDirectoryInfo.FullName, file.Name);

                file.CopyTo(backupFilePath);
            }
        }

        public void Rollback(IBackupWardenOptions opts)
        {
            var backupDirectoryPath = opts.Backup;

            var filesToRestore = _fs.DirectoryInfo
               .FromDirectoryName(GetRootBackupDirectory(backupDirectoryPath).FullName)
               .EnumerateFiles("*", opts.Search)
               .ToList();

            foreach (var file in filesToRestore)
            {
                var fileDirectoryInfo = _fs.DirectoryInfo.FromDirectoryName(file.DirectoryName.Replace(GetRootBackupDirectory(backupDirectoryPath).FullName, opts.Source));
                if (!fileDirectoryInfo.Exists)
                {
                    fileDirectoryInfo.Create();
                }
                var restoreFilePath = _fs.Path.Combine(fileDirectoryInfo.FullName, file.Name);

                file.CopyTo(restoreFilePath, true);
            }
        }

        private IDirectoryInfo GetRootBackupDirectory(string backupDirectoryPath) => _fs.DirectoryInfo.FromDirectoryName(backupDirectoryPath);
    }
}
