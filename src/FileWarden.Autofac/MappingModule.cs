using Autofac;

using AutoMapper;

using System;
using System.Linq;
using System.Reflection;

using AutofacModule = Autofac.Module;

namespace FileWarden.Autofac
{
    internal sealed class MappingModule : AutofacModule
    {
        private readonly Assembly _searchAssembly;

        public MappingModule(Assembly searchAssembly)
        {
            _searchAssembly = searchAssembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var assemblyNames = _searchAssembly.GetReferencedAssemblies();
            var assembliesTypes = assemblyNames
                .Where(a => a.Name.StartsWith("FileWarden."))
                .SelectMany(an => Assembly.Load(an).GetTypes())
                .Where(p => typeof(Profile).IsAssignableFrom(p) && p.IsPublic && !p.IsAbstract)
                .Distinct();

            var autoMapperProfiles = assembliesTypes
                .Select(p => (Profile)Activator.CreateInstance(p)).ToList();

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                foreach (var profile in autoMapperProfiles)
                {
                    cfg.AddProfile(profile);
                }
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().InstancePerLifetimeScope();
        }
    }
}
