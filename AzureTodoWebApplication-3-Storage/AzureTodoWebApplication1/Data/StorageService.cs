using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureTodoWebApplication1.Data
{
    public class StorageService : IStorageService
    {
        public CloudStorageAccount StorageAccount { get; }

        public StorageService(CloudStorageAccount storageAccount)
        {
            StorageAccount = storageAccount;
        }

        public async void CreateAndConfigureAsync()
        {
            try
            {
                // Create a blob client and retrieve reference to images container
                CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("images");

                // Create the "images" container if it doesn't already exist.
                if (await container.CreateIfNotExistsAsync())
                {
                    // Enable public access on the newly created "images" container
                    await container.SetPermissionsAsync(
                        new BlobContainerPermissions
                        {
                            PublicAccess = BlobContainerPublicAccessType.Blob
                        });
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public async Task<string> UploadPhotoAsync(IFormFile photoToUpload)
        {
            if (photoToUpload == null || photoToUpload.Length == 0)
            {
                return null;
            }

            string fullPath = null;

            try
            {
                // Create the blob client and reference the container
                CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("images");

                // Create a unique name for the images we are about to upload
                string imageName = String.Format("todo-photo-{0}{1}",
                    Guid.NewGuid().ToString(),
                    Path.GetExtension(photoToUpload.FileName));

                // Upload image to Blob Storage
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(imageName);
                blockBlob.Properties.ContentType = photoToUpload.ContentType;
                using (MemoryStream ms = new MemoryStream()) {
                    await photoToUpload.CopyToAsync(ms);
					ms.Seek(0, SeekOrigin.Begin);
                    await blockBlob.UploadFromStreamAsync(ms);
                }

                fullPath = blockBlob.Uri.AbsoluteUri;
            }
            catch (Exception ex)
            {
                
            }

            return fullPath;
        }
    }
}
