using System;
using TIK.Core.Application;
using TIK.Domain.Transaction.Bulk;

namespace TIK.Applications.Bulk.Commands.DirectDebit
{
    public interface IBulkDirectDebitCommand : IAppService
    {
        void Computation(BulkTransaction bulkTransaction);
    }
}
