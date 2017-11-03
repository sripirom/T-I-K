using System;
using TIK.Core.Logging;
using TIK.Integration.Bulk;
using TIK.Core.Application;
using TIK.Domain.Transaction.Bulk;

namespace TIK.Applications.Bulk.Commands.DirectCredit
{
    public class BulkDirectCreditCommand : BaseAppService, IBulkDirectCreditCommand
    {
        private readonly IBulkStorageService _bulkStorageService;

        public BulkDirectCreditCommand(IBulkStorageService bulkStorageService)
        {
            _bulkStorageService = bulkStorageService;
        }

        public void Computation(BulkTransaction bulkTransaction)
        {
            BulkDirectCredit bulk = _bulkStorageService.LoadJob<BulkDirectCredit>(bulkTransaction.TransactionId);

            Logger.InfoFormat("Computation Directcredit TransactionId {0}.", bulk.TransactionId);

        }

    }
}
