using System;
using TIK.Core.Application;
using TIK.Domain.Transaction.Bulk;

namespace TIK.Applications.Bulk.Commands.CreateJob
{
    public interface ICreateBulkJobCommand : IAppService
    {
        void Create(BulkTransaction bulkTransaction);
    }
}
