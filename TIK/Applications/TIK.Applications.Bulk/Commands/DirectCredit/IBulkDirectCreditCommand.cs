using System;
using TIK.Core.Application;
using TIK.Domain.Transaction.Bulk;

namespace TIK.Applications.Bulk.Commands.DirectCredit
{
    public interface IBulkDirectCreditCommand : IAppService
    {
        void Computation(BulkTransaction bulkTransaction);
    }
}
