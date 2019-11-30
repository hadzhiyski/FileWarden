using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace FileWarden.Core.Backup
{
    public class BackupWarden : IBackupWarden
    {
        private readonly IFileSystem _fs;
        private readonly string _rootBackupDirectoryPath;
        public BackupWarden(IFileSystem fs)
        {
            _fs = fs;
            _rootBackupDirectoryPath = _fs.Path.Combine(Path.GetTempPath(), "warden");
        }

        public void Cleanup()
        {
            if (GetRootBackupDirectory().Exists)
            {
                GetRootBackupDirectory().Delete(true);
            }
        }

        public void Create(IWardenBaseOptions opts)
        {
            Cleanup();

            GetRootBackupDirectory().Create();

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
                var fileBackupDirectoryInfo = _fs.DirectoryInfo.FromDirectoryName(file.DirectoryName.Replace(opts.Source, GetRootBackupDirectory().FullName));
                if (!fileBackupDirectoryInfo.Exists)
                {
                    fileBackupDirectoryInfo.Create();
                }
                var backupFilePath = _fs.Path.Combine(fileBackupDirectoryInfo.FullName, file.Name);

                file.CopyTo(backupFilePath);
            }
        }

        public void Execute(IWardenBaseOptions options) => Create(options);

        public void Restore(IWardenBaseOptions opts)
        {
            var filesToRestore = _fs.DirectoryInfo
               .FromDirectoryName(GetRootBackupDirectory().FullName)
               .EnumerateFiles("*", opts.Search)
               .ToList();

            foreach (var file in filesToRestore)
            {
                var fileDirectoryInfo = _fs.DirectoryInfo.FromDirectoryName(file.DirectoryName.Replace(GetRootBackupDirectory().FullName, opts.Source));
                if (!fileDirectoryInfo.Exists)
                {
                    fileDirectoryInfo.Create();
                }
                var restoreFilePath = _fs.Path.Combine(fileDirectoryInfo.FullName, file.Name);

                file.CopyTo(restoreFilePath, true);
            }
        }

        private IDirectoryInfo GetRootBackupDirectory() => _fs.DirectoryInfo.FromDirectoryName(_rootBackupDirectoryPath);
    }
}
