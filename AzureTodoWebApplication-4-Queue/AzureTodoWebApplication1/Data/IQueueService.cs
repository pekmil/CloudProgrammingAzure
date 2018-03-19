using AzureTodoWebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzureTodoWebApplication1.Data
{
    public interface IQueueService
    {
        Task ProcessMessagesAsync(CancellationToken token);
        Task SendMessageAsync(Todo todo);
    }
}
