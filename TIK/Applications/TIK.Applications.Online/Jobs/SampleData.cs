using System.Collections.Generic;
using TIK.Domain.Jobs;

namespace TIK.Applications.Online.Jobs
{
    public class SampleData {
        public static IEnumerable<Job> Get()
        {
            return new List<Job> {
                new Job {
                    Id = 1000,
                    Application = "Playstation 4 500GB",
                    Procedure = "Sony",
                    PricePerUnit = 29900,
                    InQueue = 5
                },
                new Job {
                    Id = 1001,
                    Application = "Playstation 4 Pro 1TB",
                    Procedure = "Sony",
                    PricePerUnit = 39900,
                    InQueue = 2
                },
                new Job {
                    Id = 1002,
                    Application = "XBOX One",
                    Procedure = "Microsoft",
                    PricePerUnit = 26700,
                    InQueue = 10
                },
                new Job {
                    Id = 1003,
                    Application = "XBOX One Scorpio",
                    Procedure = "Microsoft",
                    PricePerUnit = 499000,
                    InQueue = 1
                },
                new Job {
                    Id = 1004,
                    Application = "Wii U",
                    Procedure = "Nintendo",
                    PricePerUnit = 19900,
                    InQueue = 8
                },
            };
        }
    }
}
