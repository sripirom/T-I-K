using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TIK.Domain.Membership;
using System.Linq;

namespace TIK.Persistance.ElasticSearch.Mocks
{
    public class MockMemberRepository : MockEsRepository<Member>, IMemberRepository
    {

        public MockMemberRepository()
        {
            _collection = new List<Member> {
                new Member{
                        Id = 1,
                        UserName="user",
                        Password="password",
                        Name = new Name{ FirstName = "Tik", LastName="Sripirom"},
                        ContactInfo = new ContactInfo{ Email="pichit@sripirom.com", Phone ="0000000000"}
                    },
            };
        }

    
    }
}
