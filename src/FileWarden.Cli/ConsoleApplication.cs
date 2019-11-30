﻿using Autofac;

using AutoMapper;

using CommandLine;

using FileWarden.Cli.Options;
using FileWarden.Core.Rename.Warden;

using System;

namespace FileWarden.Cli
{
    internal class ConsoleApplication
    {
        private readonly string[] _args;
        private readonly IContainer _container;
        private readonly IMapper _mapper;

        public ConsoleApplication(string[] args, IContainer container)
        {
            _args = args;
            _container = container;
            _mapper = _container.Resolve<IMapper>();
        }

        public int Run()
        {
            return Parser.Default.ParseArguments<RenameOptions>(_args).MapResult(
                (RenameOptions opts) => ExecuteWithRenameOptions(opts),
                errs => 1);
        }

        private int ExecuteWithRenameOptions(RenameOptions opts)
        {
            var renameWarden = _container.Resolve<IRenameWarden>();

            try
            {
                var renameOptions = _mapper.Map<RenameOptions, RenameWardenOptions>(opts);

                renameWarden.Execute(renameOptions);

                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
