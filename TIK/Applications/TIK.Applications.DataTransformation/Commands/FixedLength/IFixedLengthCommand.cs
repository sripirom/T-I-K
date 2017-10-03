using System;
using System.Threading.Tasks;
using TIK.Core.Application;

namespace TIK.Applications.DataTransformation.Commands.FixedLength
{
    public interface IFixedLengthCommand : IAppService
    {
        void Transform(FixedLengthDto data);

        Task CallBack(FixedLengthDto data);
    }

    public class FixedLengthDto
    {
        public string FileName { get; set; }
        public byte[] DataStream { get; set; }
        public byte[] DataResult { get; set; }
    }
}
