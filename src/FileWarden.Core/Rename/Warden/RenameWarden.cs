using System.IO.Abstractions;

namespace FileWarden.Core.Rename.Warden
{
    public class RenameWarden : IRenameWarden
    {
        public delegate RenameWarden Factory(IFileSystem fs);

        private readonly IFileSystem _fs;

        public RenameWarden(IFileSystem fs)
        {
            _fs = fs;
        }

        public void Execute(RenameWardenOptions options)
        {
            throw new System.NotImplementedException();
        }
    }
}
