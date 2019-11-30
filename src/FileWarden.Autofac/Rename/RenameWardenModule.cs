using Autofac;

using FileWarden.Core.Rename.Builder;

namespace FileWarden.Autofac.Rename
{
    internal class RenameWardenModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<RenameWardenBuilder>().As<IRenameWardenBuilder>().InstancePerDependency();
        }
    }
}
