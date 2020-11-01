using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CodeTestDemo.Core.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace CodeTestDemo.Infrastructure.Database
{
    public class MyContextSeed
    {
        public static async Task SeedAsync(MyContext myContext,
                          ILoggerFactory loggerFactory, int retry = 0)
        {
            int retryForAvailability = retry;
            try
            {
                // TODO: Only run this if using a real database
                // myContext.Database.Migrate();

                if (!myContext.Scores.Any())
                {
                    myContext.Scores.AddRange(
                        new List<Score>{
                            new Score{
                                GameTitle = "UEFA Champions League",
                                TeamA = "Chelsea F.C.",
                                TeamB = "Real Madrid",
                                TeamAScore = 2,
                                TeamBScore = 4,
                                Employee = "Amy",
                                LastModified = DateTime.Now
                            },
                            new Score{
                                GameTitle = "UEFA Champions League 1/2",
                                TeamA = "FC Barcelona",
                                TeamB = "Man United",
                                TeamAScore = 1,
                                TeamBScore = 4,
                                Employee = "Amy",
                                LastModified = DateTime.Now
                            }
                            , new Score{
                                GameTitle = "UEFA Champions League 1/4",
                                TeamA = "Arsenal",
                                TeamB = "Real Madrid",
                                TeamAScore = 2,
                                TeamBScore = 4,
                                Employee = "Amy",
                                LastModified = DateTime.Now
                            },
                            new Score{
                                GameTitle = "UEFA Champions League 1/8",
                                TeamA = "FC Barcelona",
                                TeamB = "Man United",
                                TeamAScore = 1,
                                TeamBScore = 4,
                                Employee = "Amy",
                                LastModified = DateTime.Now
                            },
                             new Score{
                                GameTitle = "UEFA Champions League Group",
                                TeamA = "Arsenal",
                                TeamB = "Liverpool",
                                TeamAScore = 2,
                                TeamBScore = 4,
                                Employee = "Amy",
                                LastModified = DateTime.Now
                            },
                            new Score{
                                GameTitle = "UEFA Champions League 1/2",
                                TeamA = "FC Barcelona",
                                TeamB = "Man. City",
                                TeamAScore = 1,
                                TeamBScore = 4,
                                Employee = "Amy",
                                LastModified = DateTime.Now
                            }
                        }
                    );
                    await myContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var logger = loggerFactory.CreateLogger<MyContextSeed>();
                    logger.LogError(ex.Message);
                    await SeedAsync(myContext, loggerFactory, retryForAvailability);
                }
            }
        }
    }
}
