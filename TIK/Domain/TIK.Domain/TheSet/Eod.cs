using System;
using TIK.Core.Domain;

namespace TIK.Domain.TheSet
{
    public class Eod : BaseModel<string>
    {
        public Eod()
        {
        }

        public string Symbol
        {
            get;
            set;
        }

        public DateTime EodDate
        {
            get;
            set;
        }

        public decimal Open
        {
            get;
            set;
        }

        public decimal High
        {
            get;
            set;
        }

        public decimal Low
        {
            get;
            set;
        }

        public decimal Close
        {
            get;
            set;
        }

        public Int64 Volume
        {
            get;
            set;
        }
    }
}
