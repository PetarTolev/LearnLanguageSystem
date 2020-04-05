﻿namespace LearnLanguageSystem.Services.Data.Contests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Common.Repositories;
    using LearnLanguageSystem.Data.Models.Contest;
    using LearnLanguageSystem.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class ContestsService : IContestsService
    {
        private readonly IDeletableEntityRepository<Contest> contestsRepository;
        private readonly IConfiguration configuration;

        public ContestsService(IDeletableEntityRepository<Contest> contestsRepository, IConfiguration configuration)
        {
            this.contestsRepository = contestsRepository;
            this.configuration = configuration;
        }

        public IEnumerable<T> GetOwned<T>(string userId)
        {
            var contests = this.contestsRepository
                .All()
                .Where(c => c.CreatorId == userId)
                .To<T>()
                .ToList();

            return contests;
        }

        public async Task<string> CreateAsync(string name, string userId)
        {
            var contest = new Contest
            {
                CreatorId = userId,
                Name = name,
            };

            await this.contestsRepository.AddAsync(contest);
            await this.contestsRepository.SaveChangesAsync();

            return contest.Id;
        }

        public T GetById<T>(string contestId)
        {
            var contest = this.contestsRepository
                .All()
                .Where(x => x.Id == contestId)
                .To<T>()
                .FirstOrDefault();

            return contest;
        }

        public async Task ChangeNameAsync(string id, string newName)
        {
            var contest = this.contestsRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            if (contest == null)
            {
                throw new NullReferenceException();
            }

            contest.Name = newName;

            await this.contestsRepository.SaveChangesAsync();
        }

        public async Task<string> OpenAsync(string id)
        {
            var contest = await this.contestsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (contest == null)
            {
                throw new NullReferenceException();
            }

            if (contest.IsOpen)
            {
                throw new ArgumentException();
            }

            var existingCodes = await this.contestsRepository
                .All()
                .Where(x => x.AccessCode != null)
                .Select(x => x.AccessCode)
                .ToListAsync();

            var code = this.GenerateCode();

            while (existingCodes.Contains(code))
            {
                code = this.GenerateCode();
            }

            contest.AccessCode = code;
            contest.IsOpen = true;

            await this.contestsRepository.SaveChangesAsync();

            return code;
        }

        private string GenerateCode()
        {
            var codeLength = int.Parse(this.configuration["AccessCodeLength"]);

            var rnd = new Random();
            var number = rnd
                .Next(0, int.MaxValue)
                .ToString()
                .Substring(0, codeLength);

            return number;
        }
    }
}
