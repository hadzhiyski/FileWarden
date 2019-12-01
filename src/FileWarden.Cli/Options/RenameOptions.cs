using AutoMapper;

using CommandLine;

using FileWarden.Common.Extensions;
using FileWarden.Common.Mapping;
using FileWarden.Core.Rename;

namespace FileWarden.Cli.Options
{
    [Verb("rename")]
    public sealed class RenameOptions : IMapTo<RenameWardenOptions>, IMapExplicitly
    {
        [Option("source", HelpText = "Source directory", Required = true)]
        public string Source { get; set; }

        [Option("prefix", HelpText = "Prefix to append to file name", SetName = "prefix", Required = true)]
        public string Prefix { get; set; }

        [Option("suffix", HelpText = "Suffix to append to file name", SetName = "suffix", Required = true)]
        public string Suffix { get; set; }

        [Option('r', "recursive", HelpText = "When 'true' it will rename files only in top level directory, otherwise it will rename all inner files", Default = false)]
        public bool Recursive { get; set; }

        [Option('b', "backup", HelpText = "When 'true' it will create backup directory with the original directory", Default = false)]
        public bool CreateBackup { get; set; }

        [Option("no-cleanup", HelpText = "When 'true' it will not delete backup directory", Default = false)]
        public bool NoCleanup { get; set; }

        [Option('f', "force", HelpText = "When 'true' it will overwrite existing files with the same name after applying suffix / prefix", Default = false)]
        public bool OverwriteExistingFiles { get; set; }

        public void RegisterMappings(IProfileExpression profile)
        {
            profile
                .CreateMap<RenameOptions, RenameWardenOptions>()
                .ForCtorParam(nameof(RenameWardenOptions.Search).ToLower(), cfg => cfg.MapFrom(src => src.Recursive.ToSearchOption()));
        }
    }
}
