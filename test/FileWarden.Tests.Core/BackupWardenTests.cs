using File.Warden.Tests.Core.Fakes;

using FileWarden.Core.Backup;
using FileWarden.Tests.Common;

using Moq;

using NUnit.Framework;

using System.IO.Abstractions;

namespace File.Warden.Tests.Core
{
    public class BackupWardenTests
    {
        private IBackupWarden _backup;
        private FileSystemMock _fsMock;

        [SetUp]
        public void Setup()
        {
            _fsMock = new FileSystemMock();
            _backup = new BackupWarden(_fsMock.Object);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Cleanup_Deletes_Backup_Directory_If_Exists(bool exists)
        {
            const string nonExistingBackupPath = "/fake/backup";

            var opts = new TestBackupWardenOptions
            {
                Backup = nonExistingBackupPath
            };

            var backupDirectoryMock = new Mock<IDirectoryInfo>();

            _fsMock.DirectoryInfo.Setup(m => m.FromDirectoryName(It.Is<string>(c => c == nonExistingBackupPath))).Returns(backupDirectoryMock.Object);

            backupDirectoryMock.SetupGet(p => p.Exists).Returns(exists);

            _backup.Cleanup(opts);

            _fsMock.DirectoryInfo.Verify(m => m.FromDirectoryName(It.Is<string>(c => c == nonExistingBackupPath)), exists ? Times.Exactly(2) : Times.Once());
            backupDirectoryMock.Verify(m => m.Delete(It.Is<bool>(c => c == true)), exists.OnceOrNever());
            backupDirectoryMock.VerifyGet(p => p.Exists, Times.Once);
        }


        [TestCase(true)]
        [TestCase(false)]
        public void Cleanup_Done_If_In_Options(bool noCleanup)
        {
            const string backupPath = "/fake/backup";

            var opts = new TestBackupWardenOptions
            {
                Backup = backupPath,
                NoCleanup = noCleanup
            };

            var backupDirectoryMock = new Mock<IDirectoryInfo>();

            _fsMock.DirectoryInfo.Setup(m => m.FromDirectoryName(It.Is<string>(c => c == backupPath))).Returns(backupDirectoryMock.Object);

            backupDirectoryMock.SetupGet(p => p.Exists).Returns(false);

            _backup.Cleanup(opts);

            _fsMock.DirectoryInfo.Verify(m => m.FromDirectoryName(It.Is<string>(c => c == backupPath)), noCleanup.NeverOrOnce());
            backupDirectoryMock.Verify(m => m.Delete(It.IsAny<bool>()), Times.Never);
        }
    }
}