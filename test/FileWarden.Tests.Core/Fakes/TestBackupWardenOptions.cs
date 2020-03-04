using FileWarden.Core.Backup;

using System.IO;

namespace File.Warden.Tests.Core.Fakes
{
    internal class TestBackupWardenOptions : IBackupWardenOptions
    {
        public string Backup { get; set; }

        public bool NoBackup { get; set; }

        public bool NoCleanup { get; set; }

        public string Source { get; set; }

        public SearchOption Search { get; set; }
    }
}
