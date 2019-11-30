using Autofac;

using FileWarden.Autofac.Core;

using System.Reflection;

namespace FileWarden.Autofac.Cli
{
    public sealed class FileWardenCliContainerFactory
    {
        public IContainer Build(Assembly executingAssembly)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<FileSystemModule>();
            builder.RegisterModule<WardenModule>();
            builder.RegisterModule(new MappingModule(executingAssembly));

            return builder.Build();
        }
    }
}
