using Autofac;

using FileWarden.Autofac.Rename;

namespace FileWarden.Autofac.Cli
{
    public sealed class FileWardenCliContainerFactory
    {
        public IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<RenameWardenModule>();

            return builder.Build();
        }
    }
}
