using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using QueueWebJob.Data;
using QueueWebJob.Models;

namespace QueueWebJob
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("todos")] CloudQueueMessage message, TextWriter log)
        {
            log.WriteLine("TODOMSG: " + message.AsString);
            DbContextOptionsBuilder<TodoDbContext> builder = new DbContextOptionsBuilder<TodoDbContext>();
            builder.UseSqlServer(ConfigurationManager.ConnectionStrings["AzureSqlServer"].ConnectionString);
            using(TodoDbContext context = new TodoDbContext(builder.Options)){
                Todo todo = JsonConvert.DeserializeObject<Todo>(message.AsString);
                context.Add(todo);
                context.SaveChanges();
            }
        }
    }
}
