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

        public async Task<string> Upload(IFormFile file)
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

        public void Delete()
        {
            throw new System.NotImplementedException();
        }
    }
}
