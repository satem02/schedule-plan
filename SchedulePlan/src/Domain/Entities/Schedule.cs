using SchedulePlan.Domain.Common;
using SchedulePlan.Domain.Enums;
using System;

namespace SchedulePlan.Domain.Entities
{
    public class Schedule : AuditableEntity
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
