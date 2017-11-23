using System;
using TIK.Domain.Transaction.Bulk;

namespace TIK.Integration.Bulk
{
    public interface IBulkStorageService
    {
        bool StorageJob(BulkTransaction bulkTransaction);

        TBulkModel LoadJob<TBulkModel>(string transactionId)
            where TBulkModel : IBulkJobModel;
    }
}
