using SchedulePlan.Application.Common.Exceptions;
using SchedulePlan.Application.Schedules.Commands.CreateSchedule;
using SchedulePlan.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using SchedulePlan.Domain.Enums;

namespace SchedulePlan.Application.IntegrationTests.Schedules.Commands
{
    using static Testing;

    public class CreateScheduleTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateScheduleCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateSchedule()
        {
            var userId = await RunAsDefaultUserAsync();

            var command = new CreateScheduleCommand
            {
                Title = "Tasks",
                Note = "Note",
                ScheduleType = ScheduleType.Daily,
                UserId = userId
            };

            var itemId = await SendAsync(command);

            var item = await FindAsync<Schedule>(itemId);

            item.Should().NotBeNull();
            item.Title.Should().Be(command.Title);
            item.CreatedBy.Should().Be(userId);
            item.Created.Should().BeCloseTo(DateTime.Now, 10000);
            item.LastModifiedBy.Should().BeNull();
            item.LastModified.Should().BeNull();
        }
    }
}
