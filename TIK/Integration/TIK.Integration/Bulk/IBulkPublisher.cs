using System;
using TIK.Domain.Transaction.Bulk;

namespace TIK.Integration.Bulk
{
    public interface IBulkPublisher
    {
        void CreatedJob(BulkTransactionReady builTransaction);
    }
}
