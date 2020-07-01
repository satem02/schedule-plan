using FluentAssertions;
using NUnit.Framework;
using SchedulePlan.Application.Schedules.Commands.CreateSchedule;
using SchedulePlan.Application.Schedules.Queries.GetSchedules;
using SchedulePlan.Domain.Enums;
using System.Threading.Tasks;

namespace SchedulePlan.Application.IntegrationTests.Schedules.Queries
{
    using static Testing;

    public class GetSchedulesQueryTests : TestBase
    {
        [Test]
        public async Task ShouldIncludePriorityLevels()
        {
            var query = new GetSchedulesQuery();

            var result = await SendAsync(query);

            result.ScheduleTypes.Should().NotBeEmpty();
        }

        [Test]
        public async Task ShouldGetAllListsAndItems()
        {
            var query = new GetSchedulesQuery();

            var result = await SendAsync(query);

            result.Lists.Should().HaveCount(0);
        }

        [Test]
        public async Task ShouldGetOnePlanForUser()
        {
            var userId = "10";

            var command = new CreateScheduleCommand
            {
                Title = "Tasks",
                Note = "Note",
                ScheduleType = ScheduleType.Daily,
                UserId = userId
            };

            var itemId = await SendAsync(command);

            var query = new GetSchedulesQuery();
            query.UserId = userId;

            var result = await SendAsync(query);

            result.Lists.Should().HaveCount(1);
        }
    }
}
