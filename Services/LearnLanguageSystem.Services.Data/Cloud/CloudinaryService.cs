namespace LearnLanguageSystem.Services.Data.Cloud
{
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            byte[] destinationFile;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                destinationFile = memoryStream.ToArray();
            }

            ImageUploadResult uploadResult;

            using (var memoryStream = new MemoryStream(destinationFile))
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, memoryStream),
                };

                uploadResult = await this.cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult.SecureUri.AbsoluteUri;
        }

        public async Task DeleteAsync(string id)
        {
            await this.cloudinary.DestroyAsync(new DeletionParams(id));
        }

        public string GetId(string url)
        {
            var start = url.LastIndexOf('/') + 1;
            var end = url.LastIndexOf('.') - start;

            var id = url.Substring(start, end);

            return id;
        }
    }
}
