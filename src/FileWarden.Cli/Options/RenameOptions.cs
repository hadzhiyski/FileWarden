using CommandLine;

using FileWarden.Common.Mapping;
using FileWarden.Core.Rename.Warden;

namespace FileWarden.Cli.Options
{
    [Verb("rename")]
    public sealed class RenameOptions : IMapTo<RenameWardenOptions>
    {
        [Option("source", HelpText = "Source directory", Required = true)]
        public string Source { get; set; }

        //[Option('r', "prefix", HelpText = "Prefix to append to file name", SetName = "prefix", Required = true)]
        //public string Prefix { get; set; }

        [Option("suffix", HelpText = "Suffix to append to file name", SetName = "suffix", Required = true)]
        public string Suffix { get; set; }

        //[Option("ignore", HelpText = "Ignore file name regex")]
        //public string IgnoreRegex { get; set; }

        [Option('r', "recursive", HelpText = "When 'true' it will rename files only in top level directory, otherwise it will rename all inner files. Default value is 'false'", Default = false)]
        public bool Recursive { get; set; }

        [Option('b', "backup", HelpText = "When 'true' it will create backup directory with the original directory. Default value is 'false'", Default = false)]
        public bool CreateBackup { get; set; }
    }
}
