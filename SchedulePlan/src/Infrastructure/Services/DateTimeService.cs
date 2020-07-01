using SchedulePlan.Application.Common.Interfaces;
using System;

namespace SchedulePlan.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
