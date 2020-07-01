using System;
using SchedulePlan.Application.Common.Mappings;
using SchedulePlan.Domain.Entities;
using SchedulePlan.Domain.Enums;

namespace SchedulePlan.Application.Schedules.Queries.GetSchedules
{
    public class ScheduleDto : IMapFrom<Schedule>
    {
       public int Id { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public string Note { get; set; }

        public bool Done { get; set; }

        public DateTime? StartDate { get; set; }

        public ScheduleType ScheduleType { get; set; }

    }
}
