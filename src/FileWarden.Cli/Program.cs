using FileWarden.Autofac.Cli;

using System.Reflection;

namespace FileWarden.Cli
{
    class Program
    {
        static int Main(string[] args)
        {
            var container = new FileWardenCliContainerFactory().Build(Assembly.GetExecutingAssembly());

            var app = new ConsoleApplication(args, container);

            return app.Run();
        }
    }
}
