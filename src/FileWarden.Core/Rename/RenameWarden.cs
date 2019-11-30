
using FileWarden.Core.Backup;

using System;
using System.IO.Abstractions;

namespace FileWarden.Core.Rename
{
    public class RenameWarden : IRenameWarden
    {
        public delegate RenameWarden Factory(IFileSystem fs);

        private readonly IFileSystem _fs;
        private readonly IBackupWarden _backup;

        public RenameWarden(IFileSystem fs, IBackupWarden backup)
        {
            _fs = fs;
            _backup = backup;
        }

        public void Execute(RenameWardenOptions options)
        {
            if (options.CreateBackup)
            {
                _backup.Create(options);
            }

            try
            {

            }
            catch (Exception)
            {
                _backup.Restore(options);
                throw;
            }
            finally
            {
                if (!options.NoCleanup)
                {
                    _backup.Cleanup();
                }
            }
        }
    }
}
