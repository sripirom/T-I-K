using System;
using System.IO;
using System.Threading.Tasks;
using TIK.Applications.DataTransformation.Integration;

namespace TIK.Applications.DataTransformation.Commands.FixedLength
{
    public class FixedLengthCommand : IFixedLengthCommand
    {
        private readonly IDataTransformPublisher _dataTransformPublisher;
        public FixedLengthCommand(IDataTransformPublisher dataTransformPublisher)
        {
            _dataTransformPublisher = dataTransformPublisher;
        }
        public void Transform(FixedLengthDto data)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(data.DataStream))
                {
                    data.DataResult = ReadFully(ms);
                }
            }
            catch (IOException ex)
            {
                throw new ArgumentException("", ex);
            }

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
            await _dataTransformPublisher.CallBackResult(data.FileName, "Success", data.DataStream);
        }


    }
}
