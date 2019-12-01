using System.IO;

namespace FileWarden.Core
{
    public interface IWardenBaseOptions
    {
        string Source { get; }
        SearchOption Search { get; }
        string Backup { get; }
    }
}
