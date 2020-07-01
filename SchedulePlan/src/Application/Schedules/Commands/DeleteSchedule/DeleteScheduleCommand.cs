using SchedulePlan.Application.Common.Exceptions;
using SchedulePlan.Application.Common.Interfaces;
using SchedulePlan.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SchedulePlan.Application.Schedules.Commands.DeleteSchedule
{
    public class DeleteScheduleCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteScheduleCommandHandler : IRequestHandler<DeleteScheduleCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteScheduleCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Schedules.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Schedule), request.Id);
            }

            _context.Schedules.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
