using SchedulePlan.Application.Schedules.Commands.CreateSchedule;
using SchedulePlan.Application.Schedules.Commands.DeleteSchedule;
using SchedulePlan.Application.Schedules.Commands.UpdateSchedule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SchedulePlan.Application.Schedules.Queries.GetSchedules;

namespace SchedulePlan.WebAPI.Controllers
{
    //[Authorize]
    public class SchedulesController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<SchedulesVm>> Get(string userId)
        {
            return await Mediator.Send(new GetSchedulesQuery { UserId = userId });
        }
        
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateScheduleCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateScheduleCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteScheduleCommand { Id = id });

            return NoContent();
        }
    }
}
