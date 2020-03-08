namespace LearnLanguageSystem.Data.Seeding
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Models.Contest;
    using Microsoft.EntityFrameworkCore.Internal;
    using Newtonsoft.Json;

    public class ContestsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Contests.Any())
            {
                return;
            }

            var inputJson = await File.ReadAllTextAsync(@"../../Data/LearnLanguageSystem.Data/Seeding/Datasets/questions.json");

            var contest = JsonConvert.DeserializeObject<Contest>(inputJson);

            await dbContext.Contests.AddAsync(contest);
            await dbContext.SaveChangesAsync();
        }
    }
}
