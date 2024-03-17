using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoListAPI.Repositories;
using TodoListAPI.Entities;
using TodoList.Models;
using TodoList.Models.Enums;

namespace TodoListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        //api/tasks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskRepository.GetTaskList();
            var data = tasks.Select(x => new TaskDto()
                {
                    Status = x.Status,
                    Name = x.Name,
                    AssigneeId = x.AssigneeId,
                    CreatedDate = x.CreateDate,
                    Priority = x.Priority,
                    Id = x.Id,
                    AssigneeName = x.Assignee != null ? x.Assignee.Firstname + ' ' + x.Assignee.Lastname : "N/A"

            }).ToList();
            return Ok(data);
        }

        //api/tasks/xxx
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _taskRepository.GetById(id);
            if (task == null)
            {
                return NotFound($"{id} is not found");
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var task = await _taskRepository.Create(new Entities.Todo()
                {
                    Name = request.Name,
                    Status = TodoList.Models.Enums.Status.Open,
                    CreateDate = DateTime.Now,
                    Id = request.Id
                });
                return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
            }

        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, TaskUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var t = await _taskRepository.GetById(id);
                if (t == null)
                    return NotFound($"{id} is not found");
                else
                {
                    t.Name = request.Name;
                    t.Priority = request.Priority;
                    //t.Name = task.Name;
                    var taskUpdated = await _taskRepository.Update(t);

                    return Ok(new TaskDto()
                    {
                        Name = taskUpdated.Name,
                        Status = taskUpdated.Status,
                        Id = taskUpdated.Id,
                        AssigneeId = taskUpdated.AssigneeId,
                        Priority = taskUpdated.Priority,
                        CreatedDate = taskUpdated.CreateDate
                    });
                }

            }

        }

        //[HttpDelete]
        //[Route("{id}")]
        //public async Task<IActionResult> Delete([FromRoute] Guid id)
        //{
        //    var task = await _taskRepository.GetById(id);
        //    if (task == null) return NotFound($"{id} is not found");

        //    await _taskRepository.Delete(task);
        //    return Ok(new TaskDto()
        //    {
        //        Name = task.Name,
        //        Status = task.Status,
        //        Id = task.Id,
        //        AssigneeId = task.AssigneeId,
        //        Priority = task.Priority,
        //        CreatedDate = task.CreatedDate
        //    });
        //}

    }
}
