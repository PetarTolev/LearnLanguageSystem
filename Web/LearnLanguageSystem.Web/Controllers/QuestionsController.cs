namespace LearnLanguageSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using LearnLanguageSystem.Services.Data.Answers;
    using LearnLanguageSystem.Services.Data.Questions;
    using LearnLanguageSystem.Web.InputModels.Questions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class QuestionsController : BaseController
    {
        private readonly IQuestionsService questionsService;
        private readonly IAnswersService answersService;

        public QuestionsController(IQuestionsService questionsService, IAnswersService answersService)
        {
            this.questionsService = questionsService;
            this.answersService = answersService;
        }

        [Authorize]
        public IActionResult Add(string id)
        {
            return this.View();    // todo: add contests title todo: need view model with already added questions
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(string id, ContestQuestionsListInputModel model)
        {
            foreach (var question in model.Questions)
            {
                var questionId = await this.questionsService.CreateAsync(id, question.Content);

                var answerOneId = await this.answersService.CreateAsync(questionId, question.One.Content, question.One.IsRight);
                var answerTwoId = await this.answersService.CreateAsync(questionId, question.Two.Content, question.Two.IsRight);
                var answerThreeId = await this.answersService.CreateAsync(questionId, question.Three.Content, question.Three.IsRight);
                var answerFourId = await this.answersService.CreateAsync(questionId, question.Four.Content, question.Four.IsRight);
            }

            return this.View();
        }
    }
}
