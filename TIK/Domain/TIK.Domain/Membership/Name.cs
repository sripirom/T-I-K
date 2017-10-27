using System;
namespace TIK.Domain.Membership
{

    public class Name
    {
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public override string ToString()
        {
            return string.Format("[FullName:{0} {1}]", FirstName, LastName);
        }
    }
}
