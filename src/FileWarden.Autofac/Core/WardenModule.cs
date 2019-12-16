using Autofac;

using FileWarden.Core.Backup;
using FileWarden.Core.Rename;
using FileWarden.Core.Rename.Prefix;
using FileWarden.Core.Rename.Suffix;

namespace FileWarden.Autofac.Core
{
    internal sealed class WardenModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BackupWarden>().As<IBackupWarden>().InstancePerDependency();
            builder.RegisterType<RenameWarden>().As<IRenameWarden>().InstancePerDependency();
            builder.RegisterType<AppendSuffixWarden>().As<IAppendSuffixWarden>().InstancePerDependency();
            builder.RegisterType<AppendPrefixWarden>().As<IAppendPrefixWarden>().InstancePerDependency();
        }
    }
}
