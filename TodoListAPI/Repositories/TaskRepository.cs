using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TodoList.Models;
using TodoListAPI.Controllers;
using TodoListAPI.Data;
using TodoListAPI.Entities;
using TodoListAPI.Untils;

namespace TodoListAPI.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TodoListDbContext _context;
        private readonly ILogger<TaskRepository> _logger;
        public TaskRepository(TodoListDbContext context, ILogger<TaskRepository> logger) { 
            _context = context;
            _logger = logger;
        } 

        public async Task<Todo> Create(Todo task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Todo> Delete(Todo task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return task;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Todo?> GetById(Guid id)
        {
            _logger.LogInformation(MyLogEvents.GetItem, "Getting item {Id}", id);

            var todoItem = await _context.Tasks.FindAsync(id);

            if (todoItem == null)
            {
                _logger.LogWarning(MyLogEvents.GetItemNotFound, "Get({Id}) NOT FOUND", id);
                //this.Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound;
                //return NotFound();
                return null;
            }
            else
                return todoItem;
        }

        public async Task<IEnumerable<Todo>> GetTaskList()
        {                      
            return await _context.Tasks.ToListAsync();
        }

        public async Task<Todo> Update(Todo task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

  
    }
}
