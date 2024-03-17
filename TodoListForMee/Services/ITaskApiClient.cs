using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoListForMee.Services
{
    public interface ITaskApiClient
    {
        Task<List<TaskDto>> GetTaskList();
        Task<List<TaskDto>> GetTaskList(TaskListSearch taskListSearch);

        Task<List<TaskDto>> GetMyTasks(TaskListSearch taskListSearch);

        Task<TaskDto> GetTaskDetail(string id);

        Task<bool> CreateTask(TaskCreateRequest request);
        Task<bool> UpdateTask(Guid id, TaskUpdateRequest request);

        //Task<bool> AssignTask(Guid id, AssignTaskRequest request);

        Task<bool> DeleteTask(Guid id);
    }
}
