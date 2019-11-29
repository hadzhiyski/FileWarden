namespace FileWarden.Core.Rename.Builder
{
    public interface IRenameWardenBuilder : IWardenBuilder
    {
        IRenameWardenBuilder WithSource(string sourcePath);
        IRenameWardenBuilder WithSuffix(string suffix);
        IRenameWardenBuilder Recursive(bool recursive);
        IRenameWardenBuilder WithBackup(bool shouldCreateBackup);
    }
}
