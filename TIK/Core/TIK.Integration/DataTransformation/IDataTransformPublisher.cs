using System;
using System.Threading.Tasks;

namespace TIK.Integration.DataTransformation
{
    public interface IDataTransformPublisher 
    {
        Task CallBackResult(string filename, string state, byte[] dataStream);
    }
}
