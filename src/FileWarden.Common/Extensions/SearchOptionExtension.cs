using System.IO;

namespace FileWarden.Common.Extensions
{
    public static class SearchOptionExtension
    {
        public static SearchOption ToSearchOption(this bool value) =>
            value ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
    }
}
