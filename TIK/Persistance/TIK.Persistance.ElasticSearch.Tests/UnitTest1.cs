using System;
using TIK.Domain.UserAccounts;
using TIK.Persistance.ElasticSearch.Tests.Repositories;
using Xunit;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace TIK.Persistance.ElasticSearch.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }

        [Fact]
        public void SearchUserTest()
        {
            try
            {
                var context = new EsContext( new Uri("http://192.168.99.100:32809"), "member");

                //var repoIndex = new IndexRepository(context.CreateClient());
                //repoIndex.IndexData<UserAccount>(userAccount, "member", "userAccount");

                var repo = new UserAccountRepository(context.CreateClient());

                IList<Tuple<Expression<Func<UserAccount, object>>, object>> paramValue = 
                    new List<Tuple<Expression<Func<UserAccount, object>>, object>>() 
                {
                    new Tuple<Expression<Func<UserAccount, object>>, object>(q=>q.UserName, "pichit.sri"),
                    new Tuple<Expression<Func<UserAccount, object>>, object>(q=>q.Password, "passord")
                };

                var user = repo.Search(paramValue).FirstOrDefault();


                //var user = repo.Search(s => s.Query(q => q.Term(p => p.UserName, "pichit.sri"))).FirstOrDefault();

                Assert.Equal("password", user.Password);


            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public Expression<Func<UserAccount, object>> GetExp(object value)
        {
            return t => t.UserName;

        }

        [Fact]
        public void AddUserTest()
        {
            try
            {
                var userAccount = new Domain.UserAccounts.UserAccount()
                {
                    Id  = 1,
                    Password = "password",
                    UserName = "pichit.sri"
                };

                var context = new EsContext(new Uri("http://192.168.99.100:32809"), "member");

                //var repoIndex = new IndexRepository(context.CreateClient());
                //repoIndex.IndexData<UserAccount>(userAccount, "member", "userAccount");

                var repo = new UserAccountRepository(context.CreateClient());

                var id = repo.Save(userAccount);
                var key = id;
                var user = repo.Get(key);

                Assert.Equal(key, user.Id);

                var accounts = repo.List();

                //Assert.Equal(1, accounts.Count());
                //foreach (var item in accounts)
                //{
                    //repo.Delete(item.Id);
                //}
                //repo.Search(()=>async.);
            }
            catch (Exception ex)
            {
                throw ex;
            }
      

        }

        [Fact]
        public void TestMockRepository()
        {
            IList<Tuple<Expression<Func<UserAccount, object>>, object>> paramValue =
                new List<Tuple<Expression<Func<UserAccount, object>>, object>>()
            {
                new Tuple<Expression<Func<UserAccount, object>>, object>(q=>q.UserName, "user"),
                new Tuple<Expression<Func<UserAccount, object>>, object>(q=>q.Password, "password")
            };

            var repo = new Mocks.MockUserAccountRepository();

            var result = repo.Search(paramValue);

            Assert.Equal(1, result.Count());

            var member = result.FirstOrDefault(a=>a.UserName=="user");

            Assert.NotNull(member);
        }

        [Fact]
        public void TestCollectionSearch()
        {
            IList<Tuple<Expression<Func<UserAccount, object>>, object>> paramValue =
                new List<Tuple<Expression<Func<UserAccount, object>>, object>>()
            {
                new Tuple<Expression<Func<UserAccount, object>>, object>(q=>q.UserName, "user2"),
                new Tuple<Expression<Func<UserAccount, object>>, object>(q=>q.Password, "password")
            };

            var collection = new List<UserAccount> {
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

            IEnumerable<UserAccount> results = collection;
            foreach (var predicate in paramValue)
            {
                results = results.Where(a => predicate.Item1.Compile().Invoke(a) == predicate.Item2).ToList();
            }

            Assert.Equal(1, results.Count());
            var member = results.FirstOrDefault();

            Assert.NotNull(member);
        }
    }
}
