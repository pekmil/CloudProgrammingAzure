using AzureTodoWebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureTodoWebApplication1.Data
{
    public class TodoSeeder
    {
        public async static Task SeedTodos(TodoDbContext context)
        {
            if (!context.Todos.Any())
            {
                var todos = new List<Todo>
                {
                    new Todo { Id = Guid.NewGuid(), Name = "Todo 1 Name", Description = "Todo 1 Description", CreatedDate = DateTime.Now.AddDays(-1) },
                    new Todo { Id = Guid.NewGuid(), Name = "Todo 2 Name", Description = "Todo 2 Description", CreatedDate = DateTime.Now.AddDays(-11) },
                    new Todo { Id = Guid.NewGuid(), Name = "Todo 3 Name", Description = "Todo 3 Description", CreatedDate = DateTime.Now.AddDays(-6) },
                    new Todo { Id = Guid.NewGuid(), Name = "Todo 4 Name", Description = "Todo 4 Description", CreatedDate = DateTime.Now.AddDays(-3) },
                    new Todo { Id = Guid.NewGuid(), Name = "Todo 5 Name", Description = "Todo 5 Description", CreatedDate = DateTime.Now.AddDays(-7) }
                };
                context.AddRange(todos);
                await context.SaveChangesAsync();
            }
        }
    }
}
