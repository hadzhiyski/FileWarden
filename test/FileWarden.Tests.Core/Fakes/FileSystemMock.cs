using Moq;

using System.IO.Abstractions;

namespace File.Warden.Tests.Core.Fakes
{
    public class FileSystemMock : Mock<IFileSystem>
    {
        public FileSystemMock()
        {
            File = new Mock<IFile>();
            Directory = new Mock<IDirectory>();
            FileInfo = new Mock<IFileInfoFactory>();
            FileStream = new Mock<IFileStreamFactory>();
            Path = new Mock<IPath>();
            DirectoryInfo = new Mock<IDirectoryInfoFactory>();
            DriveInfo = new Mock<IDriveInfoFactory>();
            FileSystemWatcher = new Mock<IFileSystemWatcherFactory>();

            Setup(m => m.File).Returns(File.Object);
            Setup(m => m.Directory).Returns(Directory.Object);
            Setup(m => m.FileInfo).Returns(FileInfo.Object);
            Setup(m => m.FileStream).Returns(FileStream.Object);
            Setup(m => m.Path).Returns(Path.Object);
            Setup(m => m.DirectoryInfo).Returns(DirectoryInfo.Object);
            Setup(m => m.DriveInfo).Returns(DriveInfo.Object);
            Setup(m => m.FileSystemWatcher).Returns(FileSystemWatcher.Object);
        }

        public Mock<IFile> File { get; }
        public Mock<IDirectory> Directory { get; }
        public Mock<IFileInfoFactory> FileInfo { get; }
        public Mock<IFileStreamFactory> FileStream { get; }
        public Mock<IPath> Path { get; }
        public Mock<IDirectoryInfoFactory> DirectoryInfo { get; }
        public Mock<IDriveInfoFactory> DriveInfo { get; }
        public Mock<IFileSystemWatcherFactory> FileSystemWatcher { get; }
    }
}
