using System;
using TIK.Persistance.ElasticSearch;
using TIK.UnitTests.Repositories;
using Xunit;

namespace TIK.UnitTests
{
    public class RepositoryElasticTest
    {
        public RepositoryElasticTest()
        {
        }

        [Fact]
        public void AddUserTest()
        {
            var context = new EsContext(new Uri("http://192.168.99.100:32805"));
            var repo = new UserAccountRepository(context.CreateClient());

            repo.Save(new Domain.Member.UserAccount(){ FirstName = "Pichit",
             LastName="Sripirom", Password="aaaa", UserName="Pichit"});

        }
    }
}
