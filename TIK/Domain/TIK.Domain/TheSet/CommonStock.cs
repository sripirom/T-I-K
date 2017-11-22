using System;
using TIK.Core.Domain;

namespace TIK.Domain.TheSet
{
    public class CommonStock : BaseModel<Int32>
    {
        public CommonStock()
        {
        }
        public string Symbol
        {
            get;
            set;
        }

        public string Market
        {
            get;
            set;
        }
        public string SecurityName
        {
            get;
            set;
        }

 
    }
}
