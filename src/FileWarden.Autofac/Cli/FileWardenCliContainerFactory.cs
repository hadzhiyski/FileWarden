using Autofac;

using FileWarden.Autofac.Core;
using FileWarden.Autofac.Core.Rename;

namespace FileWarden.Autofac.Cli
{
    public sealed class FileWardenCliContainerFactory
    {
        public IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<FileSystemModule>();
            builder.RegisterModule<RenameWardenModule>();

            return builder.Build();
        }
    }
}
