using FluentValidation;

namespace SchedulePlan.Application.Schedules.Commands.UpdateSchedule
{
    public class UpdateScheduleCommandValidator : AbstractValidator<UpdateScheduleCommand>
    {
        public UpdateScheduleCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
