using System;
using TIK.Core.Domain;

namespace TIK.Domain.TheSet
{
    public class CommonStock : BaseModel<String>
    {
        public CommonStock()
        {
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
