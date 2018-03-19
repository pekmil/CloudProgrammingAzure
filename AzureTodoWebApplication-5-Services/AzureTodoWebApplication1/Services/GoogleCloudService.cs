using Google.Apis.Auth.OAuth2;
using Google.Cloud.Vision.V1;
using Grpc.Auth;
using Grpc.Core;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AzureTodoWebApplication1.Services
{
    public class GoogleCloudService : IGoogleCloudService
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public GoogleCloudService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<string> GetImageLabelsAsync(string imageUri)
        {
            string path = Path.Combine(_hostingEnvironment.ContentRootPath, "AzureTest-a54901545b49.json");
            GoogleCredential credential = GoogleCredential.FromFile(path).CreateScoped(ImageAnnotatorClient.DefaultScopes);
            Channel channel = new Channel(ImageAnnotatorClient.DefaultEndpoint.ToString(), credential.ToChannelCredentials());

            using (WebClient wc = new WebClient())
            {
                Image image = Image.FromBytes(await wc.DownloadDataTaskAsync(imageUri));
                ImageAnnotatorClient client = ImageAnnotatorClient.Create(channel);
                IReadOnlyList<EntityAnnotation> labels = await client.DetectLabelsAsync(image);
                string labelsString = "";
                foreach (EntityAnnotation label in labels)
                {
                    labelsString += label.Description + "|";
                }
                return labelsString;
            }
        }
    }
}
