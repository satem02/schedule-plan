using AutoMapper;
using AutoMapper.QueryableExtensions;
using SchedulePlan.Application.Common.Interfaces;
using SchedulePlan.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchedulePlan.Application.Schedules.Queries.GetSchedules
{
    public class GetSchedulesQuery : IRequest<SchedulesVm>
    {
        public string UserId { get; set; }
    }

    public class GetSchedulesQueryHandler : IRequestHandler<GetSchedulesQuery, SchedulesVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetSchedulesQueryHandler(IApplicationDbContext context, IMapper mapper,ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<SchedulesVm> Handle(GetSchedulesQuery request, CancellationToken cancellationToken)
        {
            var userId = !string.IsNullOrEmpty(request.UserId) ? request.UserId : _currentUserService.UserId;

            return new SchedulesVm
            {
                ScheduleTypes = Enum.GetValues(typeof(ScheduleType))
                    .Cast<ScheduleType>()
                    .Select(p => new ScheduleTypeDto { Value = (int)p, Name = p.ToString() })
                    .ToList(),

                Lists = await _context.Schedules
                    .Where(x => x.UserId == userId)
                    .ProjectTo<ScheduleDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Title)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
