using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListAPI.Entities;

namespace TodoListAPI.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Todo>> GetTaskList();        

        Task<Todo> Create(Todo task);

        Task<Todo> Update(Todo task);

        Task<Todo> Delete(Todo task);

        Task<Todo> GetById(Guid id);
    }
}
