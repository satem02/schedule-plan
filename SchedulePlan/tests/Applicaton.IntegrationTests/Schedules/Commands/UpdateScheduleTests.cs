using SchedulePlan.Application.Common.Exceptions;
using SchedulePlan.Application.Schedules.Commands.CreateSchedule;
using SchedulePlan.Application.Schedules.Commands.UpdateSchedule;
using SchedulePlan.Domain.Entities;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using System;
using SchedulePlan.Domain.Enums;

namespace SchedulePlan.Application.IntegrationTests.Schedules.Commands
{
    using static Testing;

    public class UpdateScheduleTests : TestBase
    {
        [Test]
        public void ShouldRequireValidScheduleId()
        {
            var command = new UpdateScheduleCommand
            {
                Id = 99,
                Title = "New Title"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateSchedule()
        {
            var userId = await RunAsDefaultUserAsync();
            var command = new CreateScheduleCommand
            {
                Title = "Tasks",
                Note = "Note",
                ScheduleType = ScheduleType.Daily,
                UserId = "1"
            };

            var itemId = await SendAsync(command);

            var updateCommand = new UpdateScheduleCommand
            {
                Id = itemId,
                Title = "Updated Item Title"
            };

            await SendAsync(command);

            var item = await FindAsync<Schedule>(itemId);

            item.Title.Should().Be(updateCommand.Title);
            item.LastModifiedBy.Should().NotBeNull();
            item.LastModifiedBy.Should().Be(userId);
            item.LastModified.Should().NotBeNull();
            item.LastModified.Should().BeCloseTo(DateTime.Now, 1000);
        }
    }
}
