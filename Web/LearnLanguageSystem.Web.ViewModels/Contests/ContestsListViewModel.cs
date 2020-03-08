using LearnLanguageSystem.Services.Mapping;

namespace LearnLanguageSystem.Web.ViewModels.Contests
{
    using System.Collections.Generic;

    public class ContestsListViewMode
    {
        public IEnumerable<ContestViewModel> Contests { get; set; }
    }
}
