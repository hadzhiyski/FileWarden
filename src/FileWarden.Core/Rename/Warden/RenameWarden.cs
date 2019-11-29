namespace FileWarden.Core.Rename.Warden
{
    public class RenameWarden : IWarden
    {
        private readonly RenameWardenOptions _options;

        public RenameWarden(RenameWardenOptions options)
        {
            _options = options;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
