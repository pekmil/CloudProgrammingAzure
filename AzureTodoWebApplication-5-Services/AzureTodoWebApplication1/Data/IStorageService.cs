using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureTodoWebApplication1.Data
{
    public interface IStorageService
    {
        void CreateAndConfigureAsync();
        Task<string> UploadPhotoAsync(IFormFile photoToUpload);
    }
}
