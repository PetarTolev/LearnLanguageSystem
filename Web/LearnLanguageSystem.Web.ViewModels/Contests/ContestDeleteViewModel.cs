namespace LearnLanguageSystem.Web.ViewModels.Contests
{
    using LearnLanguageSystem.Data.Models.Contest;
    using LearnLanguageSystem.Services.Mapping;

    public class ContestDeleteViewModel : IMapFrom<Contest>
    {
        public string Id { get; set; }
    }
}
