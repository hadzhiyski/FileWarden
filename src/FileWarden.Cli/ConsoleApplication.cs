using Autofac;

using CommandLine;

using FileWarden.Cli.Options;
using FileWarden.Core.Rename.Builder;

using System;

namespace FileWarden.Cli
{
    internal class ConsoleApplication
    {
        private readonly string[] _args;
        private readonly IContainer _container;

        public ConsoleApplication(string[] args, IContainer container)
        {
            _args = args;
            _container = container;
        }

        public int Run()
        {
            return Parser.Default.ParseArguments<RenameOptions>(_args).MapResult(
                (RenameOptions opts) => ExecuteWithRenameOptions(opts),
                errs => 1);
        }

        private int ExecuteWithRenameOptions(RenameOptions opts)
        {
            IRenameWardenBuilder renameWardenBuilder = _container.Resolve<IRenameWardenBuilder>();

            try
            {
                var warden = renameWardenBuilder
                    .WithSource(opts.Source)
                    .WithSuffix(opts.Suffix)
                    .WithBackup(opts.CreateBackup)
                    .Recursive(opts.Recursive)
                    .Build();

                warden.Execute();

                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
