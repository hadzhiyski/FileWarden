using FileWarden.Core.Commands;

using System;
using System.IO.Abstractions;

namespace FileWarden.Core.Rename.Warden
{
    public class RenameWarden : IRenameWarden
    {
        public delegate RenameWarden Factory(IFileSystem fs);

        private readonly IFileSystem _fs;
        private readonly IBackupCommand _backup;

        public RenameWarden(IFileSystem fs, IBackupCommand backup)
        {
            _fs = fs;
            _backup = backup;
        }

        public void Execute(RenameWardenOptions options)
        {
            if (options.CreateBackup)
            {
                _backup.Create(options.Source, options.Recursive);
            }

            try
            {

            }
            catch (Exception)
            {
                _backup.Restore(options.Source, options.Recursive);
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
