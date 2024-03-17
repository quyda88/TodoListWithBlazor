using TodoList.Models;

namespace TodoListForMee.Services
{
    public class TaskApiClient : ITaskApiClient
    {
        public HttpClient _httpClient;
        public TaskApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TaskDto>> GetTaskList()
        {
            var result = await _httpClient.GetFromJsonAsync<List<TaskDto>>("/api/tasks");
            return result;
        }

        public Task<List<TaskDto>> GetTaskList(TaskListSearch taskListSearch)
        {
            throw new NotImplementedException();
        }

        Task<bool> ITaskApiClient.CreateTask(TaskCreateRequest request)
        {
            throw new NotImplementedException();
        }

        Task<bool> ITaskApiClient.DeleteTask(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<List<TaskDto>> ITaskApiClient.GetMyTasks(TaskListSearch taskListSearch)
        {
            throw new NotImplementedException();
        }

        Task<TaskDto> ITaskApiClient.GetTaskDetail(string id)
        {
            throw new NotImplementedException();
        }
        Task<bool> ITaskApiClient.UpdateTask(Guid id, TaskUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
