using System;
using TIK.Core.Logging;

namespace TIK.Core.Application
{
    public abstract class BaseAppService : IAppService
    {
        public virtual ILog Logger { get; set; } = new NullLogger();
    }
}
