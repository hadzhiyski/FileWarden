
using FileWarden.Core.Backup;

using System;
using System.IO.Abstractions;

namespace FileWarden.Core.Rename
{
    public class RenameWarden : IRenameWarden
    {
        public delegate RenameWarden Factory(IFileSystem fs);

        private readonly IBackupWarden _backupWarden;
        private readonly IAppendFileNameWarden _suffixWarden;
        private readonly IAppendFileNameWarden _prefixWarden;

        public RenameWarden(IBackupWarden backupWarden, IAppendFileNameWarden suffixWarden, IAppendFileNameWarden prefixWarden)
        {
            _backupWarden = backupWarden;
            _suffixWarden = suffixWarden;
            _prefixWarden = prefixWarden;
        }

        public void Execute(RenameWardenOptions options)
        {
            if (!options.NoBackup)
            {
                _backupWarden.Create(options);
            }

            try
            {
                if (_suffixWarden.CanExecute(options))
                {
                    _suffixWarden.Execute(options);
                }

                if (_prefixWarden.CanExecute(options))
                {
                    _prefixWarden.Execute(options);
                }
            }
            catch (Exception)
            {
                if (!options.NoBackup)
                {
                    _backupWarden.Restore(options);
                }
                throw;
            }
            finally
            {
                if (!options.NoCleanup)
                {
                    _backupWarden.Cleanup(options);
                }
            }
        }
    }
}
