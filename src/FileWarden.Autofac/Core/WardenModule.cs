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

            builder.RegisterType<AppendFileNamePrefixStrategy>().Named<IAppendFileNameStrategy>(nameof(AppendFileNamePrefixStrategy)).InstancePerDependency();
            builder.RegisterType<AppendFileNameSuffixStrategy>().Named<IAppendFileNameStrategy>(nameof(AppendFileNameSuffixStrategy)).InstancePerDependency();

            builder.Register(ctx =>
                new AppendFileNameWarden(
                    ctx.Resolve<IFileSystem>(),
                    ctx.ResolveNamed<IAppendFileNameStrategy>(nameof(AppendFileNamePrefixStrategy))))
                .Named<IAppendFileNameWarden>(nameof(AppendFileNamePrefixStrategy))
                .InstancePerDependency();
            builder.Register(ctx =>
                new AppendFileNameWarden(
                    ctx.Resolve<IFileSystem>(),
                    ctx.ResolveNamed<IAppendFileNameStrategy>(nameof(AppendFileNameSuffixStrategy))))
                .Named<IAppendFileNameWarden>(nameof(AppendFileNameSuffixStrategy))
                .InstancePerDependency();

            builder.Register(ctx =>
                new RenameWarden(
                    ctx.Resolve<IBackupWarden>(),
                    ctx.ResolveNamed<IAppendFileNameWarden>(nameof(AppendFileNameSuffixStrategy)),
                    ctx.ResolveNamed<IAppendFileNameWarden>(nameof(AppendFileNamePrefixStrategy))
                    )).As<IRenameWarden>().InstancePerDependency();
        }
    }
}
