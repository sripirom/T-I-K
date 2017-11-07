using System;
using System.IO;
using System.Threading.Tasks;
using TIK.Core.Application;
using TIK.Integration.DataTransformation;

namespace TIK.Applications.DataTransformation.Commands.FixedLength
{
    public class FixedLengthCommand : BaseAppService, IFixedLengthCommand
    {
        private readonly IDataTransformPublisher _dataTransformPublisher;

        public FixedLengthCommand(IDataTransformPublisher dataTransformPublisher)
        {
            _dataTransformPublisher = dataTransformPublisher;
        }

        public Stream Transform(FixedLengthDto data)
        {
            try
            {
                if(data.DataStream != null){
                    using (MemoryStream ms = new MemoryStream(data.DataStream))
                    {
                        data.DataResult = ReadFully(ms);
                    }

                }
            
            }
            catch (IOException ex)
            {
                throw new ArgumentException("", ex);
            }

            return new MemoryStream(data.DataResult);

        }

        private byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public async Task CallBack(FixedLengthDto data)
        {
            await Task.CompletedTask;// _dataTransformPublisher.CallBackResult(data.FileName, "Success", data.DataStream);
        }


    }
}
