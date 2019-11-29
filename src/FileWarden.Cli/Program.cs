using CommandLine;

using FileWarden.Cli.Options;
using FileWarden.Core.Rename.Builder;

using System;

namespace FileWarden.Cli
{
    class Program
    {
        static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<RenameOptions>(args).MapResult(
                (RenameOptions opts) => ExecuteWithRenameOptions(opts),
                errs => 1);
        }

        private static int ExecuteWithRenameOptions(RenameOptions opts)
        {
            IRenameWardenBuilder renameWardenBuilder = new RenameWardenBuilder();

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
