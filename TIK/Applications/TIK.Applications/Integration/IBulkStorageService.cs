using System;
using TIK.Domain.Transaction.Bulk;

namespace TIK.Applications.Integration
{
    public interface IBulkStorageService
    {
        bool StorageJob(BulkTransaction bulkTransaction);

        TBulkModel LoadJob<TBulkModel>(string transactionId)
            where TBulkModel : IBulkJobModel;
    }
}
