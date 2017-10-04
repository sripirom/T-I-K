using System;
using System.IO;

namespace TIK.Domain.Transaction.Bulk
{
    public interface IBulkJobModel
    {
        string TransactionId { get; }
        Stream FileStream { get; }
    }
}
