using System;
using TIK.Domain.Member;
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
                var context = new EsContext("member", new Uri("http://192.168.99.100:32809"));

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
                var userAccount = new Domain.Member.UserAccount()
                {
                    Id  = Guid.NewGuid(),
                    FirstName = "Pichit",
                    LastName = "Sripirom",
                    Password = "password",
                    UserName = "pichit.sri"
                };

                var context = new EsContext("member", new Uri("http://192.168.99.100:32809"));

                //var repoIndex = new IndexRepository(context.CreateClient());
                //repoIndex.IndexData<UserAccount>(userAccount, "member", "userAccount");

                var repo = new UserAccountRepository(context.CreateClient());

                var id = repo.Save(userAccount);
                var key = new Guid(id);
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
    }
}
