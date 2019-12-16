using Autofac;

using FileWarden.Core;

namespace FileWarden.Autofac.Core
{
    public sealed class AutofacWardenFactory : IWardenFactory
    {
        private readonly IContainer _container;

        public AutofacWardenFactory(IContainer container)
        {
            _container = container;
        }

        public TWarden Resolve<TWarden, TOptions>()
            where TWarden : IWarden<TOptions>
            where TOptions : IWardenBaseOptions => _container.Resolve<TWarden>();
    }
}
