using System;
using System.Collections.Generic;
using TIK.Domain.Membership;

namespace TIK.Applications.Membership.Mocks
{
    public class SampleData {
        public static IList<Job> Get()
        {
            return new List<Job>  {
                new Job {
                     
                    Id = "1000",
                    Application = "SearchNews",
                    Procedure = "SearchNewsBankOfThailand",
                    Status = "000",
                    Created = new DateTime(2017, 10, 21)
                },
                new Job
                {

                    Id = "1001",
                    Application = "SearchNews",
                    Procedure = "SearchNewsBankOfThailand",
                    Status = "000",
                    Created = new DateTime(2017, 10, 22)
                },
                new Job
                {

                    Id = "1002",
                    Application = "SearchNews",
                    Procedure = "SearchNewsBankOfThailand",
                    Status = "000",
                    Created = new DateTime(2017, 10, 23)
                },
                new Job
                {

                    Id = "1003",
                    Application = "SearchNews",
                    Procedure = "SearchNewsBankOfThailand",
                    Status = "000",
                    Created = new DateTime(2017, 10, 24)
                },
                new Job
                {

                    Id = "1004",
                    Application = "SearchNews",
                    Procedure = "SearchNewsBankOfThailand",
                    Status = "000",
                    Created = new DateTime(2017, 10, 25)
                }
            };

        }
    }
}
