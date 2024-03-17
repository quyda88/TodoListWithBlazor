using Microsoft.AspNetCore.Components;
using TodoList.Models;
using TodoListForMee.Services;

namespace TodoListForMee.Components.Pages
{    
    public partial class TodoList
    {
        [Inject] private ITaskApiClient _taskApiClient {  get; set; }

        private List<TaskDto> lstTaskDto;
        protected override async Task OnInitializedAsync()
        {
            //lstTaskDto = null;
            lstTaskDto  = await _taskApiClient.GetTaskList();
            //return base.OnInitializedAsync();
        }
    }
}
