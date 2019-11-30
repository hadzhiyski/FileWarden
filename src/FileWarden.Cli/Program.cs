using FileWarden.Autofac.Cli;

namespace FileWarden.Cli
{
    class Program
    {
        static int Main(string[] args)
        {
            var container = new FileWardenCliContainerFactory().Build();

            var app = new ConsoleApplication(args, container);

            return app.Run();
        }
    }
}
