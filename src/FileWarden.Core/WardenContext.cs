using FileWarden.Core.Backup;
using FileWarden.Core.Internal;
using FileWarden.Core.Rename;

using System;
using System.Collections.Concurrent;
using System.Transactions;

namespace FileWarden.Core
{
    public class WardenContext : IWardenContext
    {
        private readonly ConcurrentDictionary<string, WardenTransactionEnlistment> _enlistments;
        private readonly IWardenFactory _wardenFactory;

        public WardenContext(IWardenFactory wardenFactory)
        {
            _wardenFactory = wardenFactory;
            _enlistments = new ConcurrentDictionary<string, WardenTransactionEnlistment>();
        }

        public void Rename(RenameWardenOptions opts)
        {
            InTransaction((id) =>
            {
                opts.WithTransactionIdentifier(id);

                var backup = _wardenFactory.Resolve<IBackupWarden, IBackupWardenOptions>();
                var backupWarden = new WardenWithOptions<IBackupWarden, IBackupWardenOptions>(backup, opts);

                var rename = _wardenFactory.Resolve<IRenameWarden, RenameWardenOptions>();
                var renameWarden = new WardenWithOptions<IRenameWarden, RenameWardenOptions>(rename, opts);

                try
                {
                    ExecuteWarden(backupWarden);
                    ExecuteWarden(renameWarden);
                }
                finally
                {
                    if (!opts.NoCleanup)
                    {
                        backup.Cleanup(opts);
                    }
                }
            });
        }

        private string GetCurrentTransactionIdentifier() => Transaction.Current.TransactionInformation.LocalIdentifier;

        private void ExecuteWarden(IWardenWithOptions warden)
        {
            if (Transaction.Current != null)
            {
                EnlistWarden(warden);
            }
            else
            {
                warden.Execute();
            }
        }

        private void EnlistWarden(IWardenWithOptions warden)
        {
            var id = GetCurrentTransactionIdentifier();
            if (!_enlistments.TryGetValue(id, out var op))
            {
                op = new WardenTransactionEnlistment();
                Transaction.Current.EnlistVolatile(op, EnlistmentOptions.None);
                _enlistments.TryAdd(id, op);
            }

            op.EnlistWarden(warden);
        }

        private void InTransaction(Action<string> action)
        {
            using var tran = new TransactionScope();
            var id = GetCurrentTransactionIdentifier();
            action(id);
            tran.Complete();
        }
    }
}
