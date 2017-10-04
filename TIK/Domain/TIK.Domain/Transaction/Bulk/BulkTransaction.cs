using System;
namespace TIK.Domain.Transaction.Bulk
{
    public class BulkTransaction
    {
        public BulkTransaction(string transactionId, string fileAddress)
        {
            TransactionId = transactionId;
            FileAddress = fileAddress;
        }

        public string TransactionId { get; private set; }

        public string FileAddress { get; private set; }

    }
}
