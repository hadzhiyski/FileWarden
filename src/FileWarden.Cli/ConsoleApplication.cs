
using AutoMapper;

using FileWarden.Cli.Options;
using FileWarden.Core;
using FileWarden.Core.Rename;

using System;

namespace FileWarden.Cli
{
    internal class ConsoleApplication
    {
        private readonly IWardenContext _ctx;
        private readonly IMapper _mapper;

        public ConsoleApplication(IMapper mapper, IWardenFactory wardenFactory)
        {
            _mapper = mapper;
            _ctx = new WardenContext(wardenFactory);
        }

        public int ExecuteWithRenameOptions(RenameOptions opts)
        {
            try
            {
                var renameOptions = _mapper.Map<RenameOptions, RenameWardenOptions>(opts);

                _ctx.Rename(renameOptions);

                return 0;
            }
            catch (Exception)
            {
                throw;
                //return -1;
            }
        }
    }
}
