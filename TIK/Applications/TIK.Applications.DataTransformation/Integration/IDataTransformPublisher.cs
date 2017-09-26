using System;
using System.Threading.Tasks;

namespace TIK.Applications.DataTransformation.Integration
{
    public interface IDataTransformPublisher
    {
        Task CallBackResult(string filename, string state, byte[] dataStream);
    }
}
