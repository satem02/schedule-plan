using FluentValidation;

namespace SchedulePlan.Application.Schedules.Commands.CreateSchedule
{
    public class CreateScheduleCommandValidator : AbstractValidator<CreateScheduleCommand>
    {
        public CreateScheduleCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
