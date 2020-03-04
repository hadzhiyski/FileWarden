using File.Warden.Tests.Core.Fakes;

using FileWarden.Core.Backup;
using FileWarden.Tests.Common;

using Moq;

using NUnit.Framework;

using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace FileWarden.Tests.Core
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

        [Test]
        public void Execute_With_NoBackup_Option_Does_Not_Do_Backup()
        {
            const string backupPath = "/fake/backup";

            var opts = new TestBackupWardenOptions
            {
                Backup = backupPath,
                NoBackup = true,
            };

            var backupDirectoryMock = new Mock<IDirectoryInfo>();
            _fsMock.DirectoryInfo.Setup(m => m.FromDirectoryName(It.Is<string>(v => v == backupPath)))
                .Returns(backupDirectoryMock.Object);
            _backup.Execute(opts);

            backupDirectoryMock.Verify(m => m.Create(), Times.Never());
        }

        [Test]
        public void Execute_Does_Cleanup_Before_Creating_Backup()
        {
            const string backupPath = "/fake/backup";
            const string sourcePath = "/fake/source";

            var opts = new TestBackupWardenOptions
            {
                Backup = backupPath,
                NoCleanup = false,
                Source = sourcePath
            };

            var backupDirectoryMock = new Mock<IDirectoryInfo>();
            backupDirectoryMock.SetupGet(p => p.Exists).Returns(true);
            _fsMock.DirectoryInfo.Setup(m => m.FromDirectoryName(It.Is<string>(c => c == backupPath))).Returns(backupDirectoryMock.Object);

            var sourceDirectoryMock = new Mock<IDirectoryInfo>();
            sourceDirectoryMock.SetupGet(p => p.Exists).Returns(true);
            sourceDirectoryMock.Setup(m => m.EnumerateFiles(It.Is<string>(v => v == sourcePath)))
                .Returns(Enumerable.Empty<IFileInfo>());
            _fsMock.DirectoryInfo.Setup(m => m.FromDirectoryName(It.Is<string>(c => c == sourcePath))).Returns(sourceDirectoryMock.Object);

            _backup.Execute(opts);

            _fsMock.DirectoryInfo.Verify(m => m.FromDirectoryName(It.Is<string>(c => c == backupPath)), Times.AtLeastOnce);
            backupDirectoryMock.Verify(m => m.Delete(It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public void Execute_Throws_Exception_When_Source_Is_Not_Found()
        {
            const string backupPath = "/fake/backup";
            const string sourcePath = "/fake/source";

            var opts = new TestBackupWardenOptions
            {
                Backup = backupPath,
                NoCleanup = false,
                Source = sourcePath
            };

            var backupDirectoryMock = new Mock<IDirectoryInfo>();
            backupDirectoryMock.SetupGet(p => p.Exists).Returns(true);
            _fsMock.DirectoryInfo.Setup(m => m.FromDirectoryName(It.Is<string>(c => c == backupPath))).Returns(backupDirectoryMock.Object);

            var sourceDirectoryMock = new Mock<IDirectoryInfo>();
            sourceDirectoryMock.SetupGet(p => p.Exists).Returns(false);
            sourceDirectoryMock.Setup(m => m.EnumerateFiles(It.Is<string>(v => v == sourcePath)))
                .Returns(Enumerable.Empty<IFileInfo>());
            _fsMock.DirectoryInfo.Setup(m => m.FromDirectoryName(It.Is<string>(c => c == sourcePath))).Returns(sourceDirectoryMock.Object);

            Assert.Throws<DirectoryNotFoundException>(() => _backup.Execute(opts));

            _fsMock.DirectoryInfo.Verify(m => m.FromDirectoryName(It.Is<string>(c => c == backupPath)), Times.AtLeastOnce);
            backupDirectoryMock.Verify(m => m.Delete(It.IsAny<bool>()), Times.Once);
        }
    }
}