using AutoMapper;

namespace FileWarden.Common.Mapping
{
    public interface IMapExplicitly
    {
        void RegisterMappings(IProfileExpression profile);
    }
}
