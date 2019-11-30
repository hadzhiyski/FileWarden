using Autofac;

using FileWarden.Core.Commands;

namespace FileWarden.Autofac.Core
{
    internal sealed class CommandsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<BackupCommand>().As<IBackupCommand>().InstancePerDependency();
        }
    }
}
