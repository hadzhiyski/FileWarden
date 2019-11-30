using Autofac;

using FileWarden.Core.Rename.Warden;

namespace FileWarden.Autofac.Core.Rename
{
    internal sealed class RenameWardenModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<RenameWarden>();
        }
    }
}
