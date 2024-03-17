using Microsoft.EntityFrameworkCore;
using TodoListAPI.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TodoListAPI.Data
{
    public class TodoListDbContext:IdentityDbContext<User,Role, Guid>
    {
        public TodoListDbContext(DbContextOptions<TodoListDbContext> options) : base(options) 
        { 
        }

        public DbSet<Todo> Tasks { get; set; }  
    }
}
