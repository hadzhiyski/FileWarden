
using FileWarden.Core.Backup;
using FileWarden.Core.Rename.Suffix;

using System;
using System.IO.Abstractions;

namespace FileWarden.Core.Rename
{
    public class RenameWarden : IRenameWarden
    {
        public delegate RenameWarden Factory(IFileSystem fs);

        private readonly IBackupWarden _backupWarden;
        private readonly IAppendSuffixWarden _suffixWarden;

        public RenameWarden(IBackupWarden backupWarden, IAppendSuffixWarden suffixWarden)
        {
            _backupWarden = backupWarden;
            _suffixWarden = suffixWarden;
        }

        public void Execute(RenameWardenOptions options)
        {
            if (options.CreateBackup)
            {
                _backupWarden.Create(options);
            }

            try
            {
                _suffixWarden.Execute(options);
            }
            catch (Exception)
            {
                _backupWarden.Restore(options);
                throw;
            }
            finally
            {
                if (!options.NoCleanup)
                {
                    _backupWarden.Cleanup();
                }
            }
        }
    }
}
