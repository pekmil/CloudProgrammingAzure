using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AzureTodoWebApplication1.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace AzureTodoWebApplication1.Data
{
    public class QueueService : IQueueService
    {
        private CloudQueueClient _queueClient;
        private TodoDbContext _context;

        private static readonly string todoQueueName = "todos";

        public QueueService(TodoDbContext context, CloudStorageAccount storageAccount)
        {
            _context = context;
            _queueClient = storageAccount.CreateCloudQueueClient();
        }
        public async Task ProcessMessagesAsync(CancellationToken token)
        {
            CloudQueue queue = _queueClient.GetQueueReference(todoQueueName);
            await queue.CreateIfNotExistsAsync();

            while (!token.IsCancellationRequested)
            {
                // The default timeout is 90 seconds, so we won’t continuously poll the queue if there are no messages. 
                // Pass in a cancellation token, because the operation can be long-running. 
                CloudQueueMessage message = await queue.GetMessageAsync();
                if (message != null)
                {
                    Todo todo = JsonConvert.DeserializeObject<Todo>(message.AsString);
                    _context.Add(todo);
                    await _context.SaveChangesAsync();
                    await queue.DeleteMessageAsync(message);
                }
            }
        }

        public async Task SendMessageAsync(Todo todo)
        {
            CloudQueue queue = _queueClient.GetQueueReference(todoQueueName);
            await queue.CreateIfNotExistsAsync();

            var todoJson = JsonConvert.SerializeObject(todo);
            CloudQueueMessage message = new CloudQueueMessage(todoJson);

            await queue.AddMessageAsync(message);
        }
    }
}
