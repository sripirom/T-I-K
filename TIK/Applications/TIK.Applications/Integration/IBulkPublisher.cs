using System;
using TIK.Domain.Transaction.Bulk;

namespace TIK.Applications.Integration
{
    public interface IBulkPublisher
    {
        void CreatedJob(BulkTransactionReady builTransaction);
    }
}
