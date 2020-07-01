using SchedulePlan.Application.Common.Exceptions;
using SchedulePlan.Application.Schedules.Commands.CreateSchedule;
using SchedulePlan.Application.Schedules.Commands.DeleteSchedule;
using SchedulePlan.Domain.Entities;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using SchedulePlan.Domain.Enums;

namespace SchedulePlan.Application.IntegrationTests.Schedules.Commands
{
    using static Testing;

    public class DeleteScheduleTests : TestBase
    {
        [Test]
        public void ShouldRequireValidScheduleId()
        {
            var command = new DeleteScheduleCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteSchedule()
        {

            var command = new CreateScheduleCommand
            {
                Title = "Tasks",
                Note = "Note",
                ScheduleType = ScheduleType.Daily,
                UserId = "1"
            };


            var itemId = await SendAsync(command);

            await SendAsync(new DeleteScheduleCommand
            {
                Id = itemId
            });

            var list = await FindAsync<Schedule>(itemId);

            list.Should().BeNull();
        }
    }
}
