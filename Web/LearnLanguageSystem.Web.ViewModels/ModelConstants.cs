namespace LearnLanguageSystem.Web.ViewModels
{
    public class ModelConstants
    {
        public const string RequiredError = "This field is required!";

        public const string NameLengthError = "Name must be between {2} and {1} symbols!";
        public const int MinNameLength = 3;
        public const int MaxNameLength = 30;

        public class Contests
        {
            public const string QuestionsCountError = "Questions count mus be between {1} and {2} symbols!";
            public const int MinQuestionsCount = 1;
            public const int MaxQuestionsCount = 10;
        }

        public class Questions
        {
            public const string ContentLengthError = "Content must be between {2} and {1} symbols!";
            public const int MinContentLength = 3;
            public const int MaxContentLength = 300;
        }

        public class Answers
        {
            public const string ContentLengthError = "Content must be between {2} and {1} symbols!";
            public const int MinContentLength = 3;
            public const int MaxContentLength = 300;
        }

        public class Contacts
        {
            public const string TitleLengthError = "Title must be between {2} and {1} symbols!";
            public const int MinTitleLength = 5;
            public const int MaxTitleLength = 100;

            public const string ContentLengthError = "Content must be between {2} and {1} symbols!";
            public const int MinContentLength = 20;
            public const int MaxContentLength = 10000;
        }
    }
}
