using Microsoft.AspNetCore.Identity;
using Polly;
using TodoList.Models.Enums;
using TodoListAPI.Entities;

namespace TodoListAPI.Data
{

    public class DbInitializer
    {
        private static readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        internal static void Initialize(TodoListDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();
            if (dbContext.Users.Any()) return;


            if (!dbContext.Tasks.Any())
            {
                dbContext.Tasks.Add(new Todo
                {
                    Id = Guid.NewGuid(),
                    Name = "Sample Task 1",
                    CreateDate = DateTime.Now,
                    Priority = Priority.Medium,
                    Status = Status.Open
                });
            }

            if (!dbContext.Users.Any())
            {
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    Firstname = "Mr",
                    Lastname = "Quan AP",
                    Email = "quanap@gmail.com",
                    PhoneNumber = "0462846843",
                    UserName = "admin"
                };

                user.PasswordHash = _passwordHasher.HashPassword(user, "Admin@123");
                dbContext.Users.Add(user);
            }

            if (!dbContext.Tasks.Any())
            {
                dbContext.Tasks.Add(new Entities.Todo()
                {
                    Id = Guid.NewGuid(),
                    Name = "Same tasks 1",
                    CreateDate = DateTime.Now,
                    Priority = Priority.High,
                    Status = Status.Open
                });
            }

            dbContext.SaveChanges();
        }
    }
}
