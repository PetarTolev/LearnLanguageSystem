namespace LearnLanguageSystem.Services.Data.Cloud
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<string> Upload(IFormFile file);

        void Delete();
    }
}
