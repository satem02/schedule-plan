using SchedulePlan.Application.Common.Interfaces;
using SchedulePlan.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using SchedulePlan.Domain.Enums;
using System;

namespace SchedulePlan.Application.Schedules.Commands.CreateSchedule
{
    public class CreateScheduleCommand : IRequest<int>
    {
        public string UserId { get; set; }

        public string Title { get; set; }

        public string Note { get; set; }

        public DateTime? StartDate { get; set; }

        public ScheduleType ScheduleType { get; set; }

    }

    public class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public CreateScheduleCommandHandler(IApplicationDbContext context,ICurrentUserService currentUserService, IDateTime dateTime)
        {
            _context = context;
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public async Task<int> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
        {
            var userId = !string.IsNullOrEmpty(request.UserId) ? request.UserId : _currentUserService.UserId;

            var entity = new Schedule
            {
                UserId = userId,
                Title = request.Title,
                Note = request.Note,
                ScheduleType = request.ScheduleType,
                StartDate = DateTime.MinValue == request.StartDate ? _dateTime.Now : request.StartDate,
                Done = false
            };

            _context.Schedules.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
