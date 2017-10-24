using System;
namespace TIK.Domain.Member
{
    public class ContactInfo
    {
        public ContactInfo()
        {
        }

        public string Email
        {
            get;
            set;
        }
        public string Phone
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("[ContactInfo: Email={0}, Phone={1}]", Email, Phone);
        }
    }
}
