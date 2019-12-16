namespace FileWarden.Core.Internal
{
    internal interface IWardenWithOptions
    {
        void Execute();

        void Rollback();
    }
}
