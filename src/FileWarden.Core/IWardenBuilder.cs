namespace FileWarden.Core
{
    public interface IWardenBuilder
    {
        void Reset();
        IWarden Build();
    }
}
