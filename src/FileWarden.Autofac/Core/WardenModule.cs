using Autofac;

using FileWarden.Core.Backup;
using FileWarden.Core.Rename;
using FileWarden.Core.Rename.Prefix;
using FileWarden.Core.Rename.Suffix;

using System.IO.Abstractions;

namespace FileWarden.Autofac.Core
{
    internal sealed class WardenModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BackupWarden>().As<IBackupWarden>().InstancePerDependency();

            builder.RegisterType<AppendFileNamePrefixFormatter>().Named<IAppendFileNameStrategy>(nameof(AppendFileNamePrefixFormatter)).InstancePerDependency();
            builder.RegisterType<AppendFileNameSuffixFormatter>().Named<IAppendFileNameStrategy>(nameof(AppendFileNameSuffixFormatter)).InstancePerDependency();

            builder.Register(ctx =>
                new AppendFileNameWarden(
                    ctx.Resolve<IFileSystem>(),
                    ctx.ResolveNamed<IAppendFileNameStrategy>(nameof(AppendFileNamePrefixFormatter))))
                .Named<IAppendFileNameWarden>(nameof(AppendFileNamePrefixFormatter))
                .InstancePerDependency();
            builder.Register(ctx =>
                new AppendFileNameWarden(
                    ctx.Resolve<IFileSystem>(),
                    ctx.ResolveNamed<IAppendFileNameStrategy>(nameof(AppendFileNameSuffixFormatter))))
                .Named<IAppendFileNameWarden>(nameof(AppendFileNameSuffixFormatter))
                .InstancePerDependency();

            builder.Register(ctx =>
                new RenameWarden(
                    ctx.Resolve<IBackupWarden>(),
                    ctx.ResolveNamed<IAppendFileNameWarden>(nameof(AppendFileNameSuffixFormatter)),
                    ctx.ResolveNamed<IAppendFileNameWarden>(nameof(AppendFileNamePrefixFormatter))
                    )).As<IRenameWarden>().InstancePerDependency();
        }
    }
}
