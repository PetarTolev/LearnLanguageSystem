namespace LearnLanguageSystem.Web.ViewModels
{
    public class ModelConstants
    {
        public const string RequiredError = "This field is required!";

        public class Contests
        {
            public const string NameLengthError = "Name must be between {2} and {1} symbols!";
            public const int MinNameLength = 3;
            public const int MaxNameLength = 30;

            public const string QuestionsCountError = "Questions count mus be between {2} and {1} symbols!";
            public const int MinQuestionsCount = 1;
            public const int MaxQuestionsCount = 10;
        }
    }
}
