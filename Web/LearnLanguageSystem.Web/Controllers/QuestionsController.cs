namespace LearnLanguageSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Data.Answers;
    using LearnLanguageSystem.Services.Data.Questions;
    using LearnLanguageSystem.Web.Filters;
    using LearnLanguageSystem.Web.ViewModels.Questions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class QuestionsController : BaseController
    {
        private readonly IQuestionsService questionsService;
        private readonly IAnswersService answersService;
        private readonly UserManager<ApplicationUser> userManager;

        public QuestionsController(IQuestionsService questionsService, IAnswersService answersService, UserManager<ApplicationUser> userManager)
        {
            this.questionsService = questionsService;
            this.answersService = answersService;
            this.userManager = userManager;
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

        [ServiceFilter(typeof(IdExistValidation))]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var creatorId = await this.questionsService.GetCreatorId(id);

            if (user.Id != creatorId)
            {
                return this.Forbid();
            }

            var model = await this.questionsService.GetByIdAsync<QuestionEditViewModel>(id);

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(IdExistValidation))]
        public async Task<IActionResult> Edit(QuestionEditViewModel model)
        {
            var questionId = await this.questionsService.UpdateAsync(model);

            return this.RedirectToAction(nameof(ContestsController.Edit), "Contests", new { id = questionId });
        }

        [ServiceFilter(typeof(IdExistValidation))]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var creatorId = await this.questionsService.GetCreatorId(id);

            if (user.Id != creatorId)
            {
                return this.Forbid();
            }

            var deletedQuestionId = await this.questionsService.DeleteAsync(id);

            return this.RedirectToAction(nameof(ContestsController.Edit), "Contests", new { id = deletedQuestionId });
        }
    }
}
