namespace LearnLanguageSystem.Web.ViewModels
{
    public class ModelConstants
    {
        public const string NameLengthError = "Name must be between {2} and {1} symbols!";

        public class Contests
        {
            public const int MinLength = 3;
            public const int MaxLength = 30;
        }

        public class Questions
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 70;
        }

        public class Answers
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 70;
        }
    }
}
