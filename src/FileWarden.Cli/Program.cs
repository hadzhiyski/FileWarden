using CommandLine;

using FileWarden.Autofac.Cli;
using FileWarden.Cli.Options;

using System.Reflection;

namespace FileWarden.Cli
{
    class Program
    {
        static int Main(string[] args)
        {
            var container = new FileWardenCliContainerFactory().Build(Assembly.GetExecutingAssembly());

            var app = new ConsoleApplication(args, container);

            return Parser.Default.ParseArguments<RenameOptions>(args).MapResult(
                (RenameOptions opts) => app.ExecuteWithRenameOptions(opts),
                errs => 1);
        }
    }
}
