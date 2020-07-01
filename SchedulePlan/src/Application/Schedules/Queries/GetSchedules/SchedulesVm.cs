using System.Collections.Generic;

namespace SchedulePlan.Application.Schedules.Queries.GetSchedules
{
    public class SchedulesVm
    {
        public IList<ScheduleTypeDto> ScheduleTypes { get; set; }

        public IList<ScheduleDto> Lists { get; set; }
    }
}
