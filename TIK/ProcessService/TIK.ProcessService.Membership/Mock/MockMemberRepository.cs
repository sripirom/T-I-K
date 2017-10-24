using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TIK.Domain.Member;
using System.Linq;

namespace TIK.ProcessService.Membership.Mock
{
    public class MockMemberRepository : IMemberRepository
    {
        private IList<Member> _members;
        public MockMemberRepository()
        {
            _members = new List<Member> { 
                new Member{
                        Id = 1,
                        Name = new Name{ FirstName = "Tik", LastName="Sripirom"},
                        ContactInfo = new ContactInfo{ Email="pichit@sripirom.com", Phone ="0000000000"}
                    },
            };
        }

        public Member Get(int id)
        {
            return _members.FirstOrDefault(a => a.Id == id);
        }
        public IEnumerable<Member> List()
        {
            return _members;
        }
        public IEnumerable<Member> Search(IEnumerable<Tuple<Expression<Func<Member, object>>, object>> paramValue)
        {
            return _members;//.Where(paramValue.FirstOrDefault().Item1.Compile());
        }
        public bool Delete(int id)
        {
            var member = _members.FirstOrDefault(a => a.Id == id);
            if(member!=null){
                return _members.Remove(member); 
            }else{

                return false;
            }

        }
        public Int32 Save(Member entry)
        {
            if(entry.Id==0){
                entry.Id = _members.Max(a => a.Id) + 1;
            }
            var member = _members.FirstOrDefault(a => a.Id == entry.Id);
            if(member==null)
            {
                _members.Add(entry); 
            }else{
                member.Name = entry.Name;
                member.ContactInfo = entry.ContactInfo;
            }
            return entry.Id;
        }


    }
}
