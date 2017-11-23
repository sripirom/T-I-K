using System;
using System.Threading.Tasks;
using TIK.Integration.DataTransformation;

namespace TIK.Computation.Hangfire.Integration
{
    public class DataTransformPublisher : IDataTransformPublisher
    {
        public DataTransformPublisher()
        {
        }

        public Task CallBackResult(string filename, string state, byte[] dataStream)
        {
            return Task.Run(() => { 
            
            }); 
        }
    }
}
