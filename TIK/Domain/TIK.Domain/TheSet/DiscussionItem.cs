using System;
using TIK.Core.Domain;

namespace TIK.Domain.TheSet
{
    public class DiscussionItem : BaseModel<Int32>
    {
        public DiscussionItem()
        {
        }


        public string UserName
        {
            get;
            set;
        }
        public DateTime EnteredOn
        {
            get;
            set;
        }

        public string Comment
        {
            get;
            set;
        }
    }
}
