namespace LearnLanguageSystem.Data.Models
{
    using LearnLanguageSystem.Data.Common.Models;

    public class EmailContact : BaseModel<int>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
