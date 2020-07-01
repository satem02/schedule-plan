using SchedulePlan.Application.Common.Exceptions;
using SchedulePlan.Application.Common.Interfaces;
using SchedulePlan.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SchedulePlan.Application.Schedules.Commands.UpdateSchedule
{
    public partial class UpdateScheduleCommand : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool Done { get; set; }
    }

    public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateScheduleCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Schedules.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Schedule), request.Id);
            }

            entity.Title = request.Title;
            entity.Done = request.Done;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
