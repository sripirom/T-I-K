using System;
using TIK.Applications.Integration;
using TIK.Core.Application;
using TIK.Core.Logging;
using TIK.Domain.Transaction.Bulk;

namespace TIK.Applications.Bulk.Commands.DirectDebit
{
    public class BulkDirectDebitCommand : BaseAppService, IBulkDirectDebitCommand
    {
        private readonly IBulkStorageService _bulkStorageService;

        public BulkDirectDebitCommand(IBulkStorageService bulkStorageService)
        {
            _bulkStorageService = bulkStorageService;
        }

        public void Computation(BulkTransaction bulkTransaction)
        {
            BulkDirectCredit bulk = _bulkStorageService.LoadJob<BulkDirectCredit>(bulkTransaction.TransactionId);

            Logger.InfoFormat("Computation DirectDebit TransactionId {0}.", bulk.TransactionId);
        }
    }
}
