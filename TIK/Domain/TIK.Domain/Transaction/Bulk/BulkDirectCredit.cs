using System;
using System.IO;

namespace TIK.Domain.Transaction.Bulk
{
    public class BulkDirectCredit : IBulkJobModel
    {
        public BulkDirectCredit(string transactionId, Stream fileStream)
        {
            TransactionId = transactionId;
            FileStream = fileStream;
        }

        public string TransactionId { get; private set; }
        public Stream FileStream { get; private set; }
    }
}
 