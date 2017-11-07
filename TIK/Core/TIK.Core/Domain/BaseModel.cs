using System;
namespace TIK.Core.Domain
{
    public class BaseModel<TId>
    {
        public BaseModel()
        {
        }

        public TId Id
        {
            get;
            set;
        }
    }
}
