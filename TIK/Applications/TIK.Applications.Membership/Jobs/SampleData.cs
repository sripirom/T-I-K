using System.Collections.Generic;

namespace TIK.Applications.Membership.Jobs
{
    public class SampleData {
        public static IEnumerable<Job> Get()
        {
            return new List<Job> {
                new Job {
                    Id = 1000,
                    Title = "Playstation 4 500GB",
                    Brand = "Sony",
                    PricePerUnit = 29900,
                    InStock = 5
                },
                new Job {
                    Id = 1001,
                    Title = "Playstation 4 Pro 1TB",
                    Brand = "Sony",
                    PricePerUnit = 39900,
                    InStock = 2
                },
                new Job {
                    Id = 1002,
                    Title = "XBOX One",
                    Brand = "Microsoft",
                    PricePerUnit = 26700,
                    InStock = 10
                },
                new Job {
                    Id = 1003,
                    Title = "XBOX One Scorpio",
                    Brand = "Microsoft",
                    PricePerUnit = 499000,
                    InStock = 1
                },
                new Job {
                    Id = 1004,
                    Title = "Wii U",
                    Brand = "Nintendo",
                    PricePerUnit = 19900,
                    InStock = 8
                },
            };
        }
    }
}
