using System.Collections.Generic;
using TIK.Domain.Jobs;

namespace TIK.Applications.Online.Jobs
{
    public class SampleData {
        public static IEnumerable<Job> Get()
        {
            return new List<Job> {
                new Job {
                    Id = "1000",
                    Application = "Playstation 4 500GB",
                    Procedure = "Sony",
                    Status = "Success",
                    InQueue = 5
                },
                new Job {
                    Id = "1001",
                    Application = "Playstation 4 Pro 1TB",
                    Procedure = "Sony",
                    Status = "Success",
                    InQueue = 2
                },
                new Job {
                    Id = "1002",
                    Application = "XBOX One",
                    Procedure = "Microsoft",
                    Status = "Success",
                    InQueue = 10
                },
                new Job {
                    Id = "1003",
                    Application = "XBOX One Scorpio",
                    Procedure = "Microsoft",
                    Status = "Success",
                    InQueue = 1
                },
                new Job {
                    Id = "1004",
                    Application = "Wii U",
                    Procedure = "Nintendo",
                    Status = "Success",
                    InQueue = 8
                },
            };
        }
    }
}
