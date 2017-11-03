using System;
using TIK.Integration.Bulk;
using TIK.Core.Application;
using TIK.Domain.Transaction.Bulk;

namespace TIK.Applications.Bulk.Commands.CreateJob
{
    public class CreateBulkJobCommand : BaseAppService, ICreateBulkJobCommand
    {
        private readonly IBulkStorageService _bulkStorageService;
        private readonly IBulkPublisher _bulkPublisher;

        public CreateBulkJobCommand(IBulkPublisher bulkPublisher, IBulkStorageService bulkStorageService)
        {
            _bulkPublisher = bulkPublisher;
            _bulkStorageService = bulkStorageService;
        }

        public void Create(BulkTransaction bulkTransaction)
        {
            _bulkStorageService.StorageJob(bulkTransaction);

            string bulkType = GetBulkType(bulkTransaction);

            _bulkPublisher.CreatedJob(new BulkTransactionReady
                                      (bulkTransaction.TransactionId, 
                                       bulkTransaction.FileAddress, bulkType));
        }

        private string GetBulkType(BulkTransaction bulkTransaction)
        {
            return "DirectCredit";
        }
    }
}
