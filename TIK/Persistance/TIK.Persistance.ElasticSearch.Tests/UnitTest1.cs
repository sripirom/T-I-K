using System;
using TIK.Domain.Member;
using TIK.Persistance.ElasticSearch.Repository;
using TIK.Persistance.ElasticSearch.Tests.Repositories;
using Xunit;

namespace TIK.Persistance.ElasticSearch.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }

        [Fact]
        public void AddUserTest()
        {
            try
            {
                var userAccount = new Domain.Member.UserAccount()
                {
                    FirstName = "Pichit",
                    LastName = "Sripirom",
                    Password = "aaaa",
                    UserName = "Pichit"
                };

                var context = new EsContext(new Uri("http://192.168.99.100:32805"));

                //var repoIndex = new IndexRepository(context.CreateClient());
                //repoIndex.IndexData<UserAccount>(userAccount, "member", "userAccount");

                var repo = new UserAccountRepository(context.CreateClient());

                var id = repo.Save(userAccount);

                var accounts = repo.List();

                foreach (var item in accounts)
                {
                    repo.Delete(item.Id);
                }
                //repo.Search(()=>async.);
            }
            catch (Exception ex)
            {
                throw ex;
            }
      

        }
    }
}
