using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureTodoWebApplication1.Services
{
    public interface IGoogleCloudService
    {
        Task<string> GetImageLabelsAsync(string imageUri);
    }
}
