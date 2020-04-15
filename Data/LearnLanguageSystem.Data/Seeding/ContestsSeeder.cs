namespace LearnLanguageSystem.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public class ContestsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            //if (dbContext.Contests.Any())
            //{
            //    return;
            //}

            //var inputJson = await File.ReadAllTextAsync(@"../../Data/LearnLanguageSystem.Data/Seeding/Datasets/questions.json");

            //var contest = JsonConvert.DeserializeObject<Contest>(inputJson);

            //await dbContext.Contests.AddAsync(contest);
            //await dbContext.SaveChangesAsync();
        }
    }
}
