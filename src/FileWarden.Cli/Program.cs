using Autofac;

using AutoMapper;

using CommandLine;

using FileWarden.Autofac.Cli;
using FileWarden.Autofac.Core;
using FileWarden.Cli.Options;

using System.Reflection;

namespace FileWarden.Cli
{
    class Program
    {
        static int Main(string[] args)
        {
            var container = new FileWardenCliContainerFactory().Build(Assembly.GetExecutingAssembly());

            var mapper = container.Resolve<IMapper>();

            var app = new ConsoleApplication(mapper, new AutofacWardenFactory(container));

            return Parser.Default.ParseArguments<RenameOptions>(args).MapResult(
                (RenameOptions opts) => app.ExecuteWithRenameOptions(opts),
                errs => 1);
        }
    }
}
