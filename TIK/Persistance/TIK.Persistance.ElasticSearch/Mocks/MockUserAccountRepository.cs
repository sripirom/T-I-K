using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TIK.Domain.UserAccounts;
using System.Linq;

namespace TIK.Persistance.ElasticSearch.Mocks
{
    public class MockUserAccountRepository : MockEsRepository<UserAccount, Int32>, IUserAccountRepository
    {
        public MockUserAccountRepository()
        {
            _collection = new List<UserAccount> {
                new UserAccount{
                        Id = 1,
                        TokenId = "",
                        UserName="user1",
                        Password="password"
                },
                new UserAccount{
                        Id = 2,
                        TokenId = "",
                        UserName="user2",
                        Password="password"
                },
                new UserAccount{
                        Id = 3,
                        TokenId = "",
                        UserName="user3",
                        Password="password"
                }
            };
        } 

    }
}
