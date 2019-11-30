using Autofac;

using System.IO.Abstractions;

namespace FileWarden.Autofac.Core
{
    internal sealed class FileSystemModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<FileSystem>().As<IFileSystem>().InstancePerDependency();
        }
    }
}
