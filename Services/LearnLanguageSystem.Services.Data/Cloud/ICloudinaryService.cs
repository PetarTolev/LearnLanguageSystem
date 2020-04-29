namespace LearnLanguageSystem.Services.Data.Cloud
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<string> UploadAsync(IFormFile file);

        Task DeleteAsync(string id);

        string GetId(string url);
    }
}
