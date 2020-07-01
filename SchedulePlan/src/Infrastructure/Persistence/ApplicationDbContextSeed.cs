using SchedulePlan.Domain.Entities;
using SchedulePlan.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using SchedulePlan.Domain.Enums;

namespace SchedulePlan.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "Administrator1!");
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.Schedules.Any())
            {
                context.Schedules.Add(new Schedule
                {
                    UserId = "1",
                    Note = "Note",
                    Title = "title",
                    ScheduleType = ScheduleType.Daily
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
