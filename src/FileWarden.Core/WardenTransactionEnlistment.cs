using FileWarden.Core.Internal;

using System;
using System.Collections.Concurrent;
using System.Transactions;

namespace FileWarden.Core
{
    internal sealed class WardenTransactionEnlistment : IEnlistmentNotification
    {
        private readonly ConcurrentStack<IWardenWithOptions> _journal;

        public WardenTransactionEnlistment()
        {
            _journal = new ConcurrentStack<IWardenWithOptions>();
        }

        public void Commit(Enlistment enlistment)
        {
            DisposeJournal(false);

            enlistment.Done();
        }

        public void InDoubt(Enlistment enlistment) => Rollback(enlistment);

        public void Prepare(PreparingEnlistment preparingEnlistment) => preparingEnlistment.Prepared();

        public void Rollback(Enlistment enlistment)
        {
            try
            {
                DisposeJournal(true);
            }
            catch (Exception e)
            {
                throw new TransactionException("Failed to roll back.", e);
            }

            enlistment.Done();
        }

        public void EnlistWarden(IWardenWithOptions wardenWithOptions)
        {
            wardenWithOptions.Execute();

            _journal.Push(wardenWithOptions);
        }

        private void DisposeJournal(bool rollback)
        {

            while (!_journal.IsEmpty)
            {
                if (_journal.TryPop(out var op))
                {
                    if (rollback)
                    {
                        op.Rollback();
                    }

                    if (op is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }
    }
}
