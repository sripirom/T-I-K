using System;
using TIK.Core.Domain;
using System.Collections.Generic;

namespace TIK.Domain.Member
{
    public class Member : BaseModel<Int32>
    {
        public String UserName { get; set; }
        public String Password { get; set; }

        public Name Name { get; set; }

        public ContactInfo ContactInfo { get; set; }

        public bool IsActive { get; set; }

    }


}
