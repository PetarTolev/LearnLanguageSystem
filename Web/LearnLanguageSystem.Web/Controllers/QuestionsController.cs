namespace LearnLanguageSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using LearnLanguageSystem.Services.Data.Answers;
    using LearnLanguageSystem.Services.Data.Questions;
    using LearnLanguageSystem.Web.ViewModels.Answers;
    using LearnLanguageSystem.Web.ViewModels.Questions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class QuestionsController : BaseController
    {
        private readonly IQuestionsService questionsService;
        private readonly IAnswersService answersService;

        public QuestionsController(IQuestionsService questionsService, IAnswersService answersService)
        {
            this.questionsService = questionsService;
            this.answersService = answersService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromRoute(Name = "id")]string contestId, ContestQuestionsListInputModel model)
        {
            foreach (var question in model.Questions)
            {
                var questionId = await this.questionsService.CreateAsync(contestId, question.Content);

                await this.answersService.CreateAsync(questionId, question.One.Content, question.One.IsRight);
                await this.answersService.CreateAsync(questionId, question.Two.Content, question.Two.IsRight);
                await this.answersService.CreateAsync(questionId, question.Three.Content, question.Three.IsRight);
                await this.answersService.CreateAsync(questionId, question.Four.Content, question.Four.IsRight);
            }

            return this.RedirectToAction("Edit", "Contests", new { id = contestId });
        }

        public IActionResult Edit(string id)
        {
            var model = new QuestionInputModel
            {
                Content = "test",
                One = new AnswerInputModel { IsRight = false, Content = "1" },
                Two = new AnswerInputModel { IsRight = false, Content = "2" },
                Three = new AnswerInputModel { IsRight = true, Content = "3" },
                Four = new AnswerInputModel { IsRight = false, Content = "4" },
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(QuestionInputModel model)
        {
            return this.View(model);
        }

        public IActionResult Delete(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
