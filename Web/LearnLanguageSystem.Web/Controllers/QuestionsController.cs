namespace LearnLanguageSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Common.Repositories;
    using LearnLanguageSystem.Data.Models.Contest;
    using LearnLanguageSystem.Services.Data.Answers;
    using LearnLanguageSystem.Services.Data.Questions;
    using LearnLanguageSystem.Services.Mapping;
    using LearnLanguageSystem.Web.Filters;
    using LearnLanguageSystem.Web.ViewModels.Answers;
    using LearnLanguageSystem.Web.ViewModels.Questions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Authorize]
    public class QuestionsController : BaseController
    {
        private readonly IQuestionsService questionsService;
        private readonly IAnswersService answersService;
        private readonly IDeletableEntityRepository<Question> questionRepository;

        public QuestionsController(
            IQuestionsService questionsService,
            IAnswersService answersService,
            IDeletableEntityRepository<Question> questionRepository)
        {
            this.questionsService = questionsService;
            this.answersService = answersService;
            this.questionRepository = questionRepository;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateValidation]
        public async Task<IActionResult> Add(ContestQuestionsListInputModel model)
        {
            foreach (var question in model.Questions)
            {
                var questionId = await this.questionsService.CreateAsync(model.Id, question.Content);

                await this.answersService.CreateAsync(questionId, question.One.Content, question.One.IsRight);
                await this.answersService.CreateAsync(questionId, question.Two.Content, question.Two.IsRight);
                await this.answersService.CreateAsync(questionId, question.Three.Content, question.Three.IsRight);
                await this.answersService.CreateAsync(questionId, question.Four.Content, question.Four.IsRight);
            }

            return this.RedirectToAction(nameof(ContestsController.Edit), "Contests", new { id = model.Id });
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return this.BadRequest();
            }

            var model = await this.questionsService.GetByIdAsync<QuestionEditViewModel>(id);

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(QuestionEditViewModel model)
        {
            if (model.Id == null)
            {
                return this.NotFound();
            }

            var question = await this.questionsService.GetWithAnswerAsync(model.Id);

            question.Content = model.Content;

            var i = 0;
            foreach (var answer in question.Answers)
            {
                answer.Content = model.Answers[i].Content;
                answer.IsRight = model.Answers[i].IsRight;

                i++;
            }

            await this.questionRepository.SaveChangesAsync();

            return this.RedirectToAction(nameof(ContestsController.Edit), "Contests", new { id = question.ContestId });
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var deletedQuestionId = await this.questionsService.DeleteAsync(id);

            return this.RedirectToAction(nameof(ContestsController.Edit), "Contests", new { id = deletedQuestionId });
        }
    }
}
