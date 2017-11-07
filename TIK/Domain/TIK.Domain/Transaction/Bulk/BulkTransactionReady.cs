using System;
namespace TIK.Domain.Transaction.Bulk
{
    public class BulkTransactionReady
    {
        public BulkTransactionReady(string transactionId, string fileAddress, string bulkType)
        {
            TransactionId = transactionId;
            FileAddress = fileAddress;
            BulkType = bulkType;
        }

        public string TransactionId { get; private set; }
        public string FileAddress { get; private set; }
        public string BulkType { get; private set; }
    }
}
