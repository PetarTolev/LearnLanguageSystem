namespace LearnLanguageSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Data.Answers;
    using LearnLanguageSystem.Services.Data.Contests;
    using LearnLanguageSystem.Services.Data.Questions;
    using LearnLanguageSystem.Web.Filters;
    using LearnLanguageSystem.Web.ViewModels.Questions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class QuestionsController : BaseController
    {
        private const int MaxCount = 10;
        private readonly IQuestionsService questionsService;
        private readonly IAnswersService answersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IContestsService contestsService;

        public QuestionsController(
            IContestsService contestsService,
            IQuestionsService questionsService,
            IAnswersService answersService,
            UserManager<ApplicationUser> userManager)
        {
            this.questionsService = questionsService;
            this.answersService = answersService;
            this.userManager = userManager;
            this.contestsService = contestsService;
        }

        public async Task<IActionResult> Add(string contestId, int? questionsCount)
        {
            if (contestId == null || questionsCount == 0 || questionsCount == null)
            {
                return this.BadRequest();
            }

            var creatorId = this.contestsService.GetCreatorId(contestId);
            var user = await this.userManager.GetUserAsync(this.User);

            if (creatorId != user.Id)
            {
                return this.Forbid();
            }

            var currentQuestionsCount = this.contestsService.GetQuestionsCount(contestId);

            if (currentQuestionsCount == 10)
            {
                this.TempData["Notification"] = $"You have reached the questions limit!";
                return this.RedirectToAction(nameof(ContestsController.Edit), "Contests", new { id = contestId });
            }

            var questionsCountForAdd = questionsCount.Value;
            var reminingQuestionsCount = MaxCount - (currentQuestionsCount + questionsCountForAdd);

            if (reminingQuestionsCount < 0)
            {
                var allowedQuestionsCount = MaxCount - currentQuestionsCount;
                this.TempData["Notification"] = $"You have exceeded the questions limit. You can only add {allowedQuestionsCount} questions.";
                questionsCountForAdd = allowedQuestionsCount;
            }

            var model = new ContestQuestionsListInputModel
            {
                Id = contestId,
                Questions = new QuestionInputModel[questionsCountForAdd],
            };

            return this.View("Add", model);
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

        [IdExistValidation]
        [ServiceFilter(typeof(OwnershipValidation))]
        public IActionResult Edit(string id)
        {
            var question = this.questionsService.GetById<QuestionEditViewModel>(id);

            if (question == null)
            {
                return this.NotFound();
            }

            return this.View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(QuestionEditViewModel model)
        {
            var contestId = await this.questionsService.UpdateAsync(model);

            if (contestId == null)
            {
                return this.NotFound();
            }

            return this.RedirectToAction(nameof(ContestsController.Edit), "Contests", new { id = contestId });
        }

        [IdExistValidation]
        [ServiceFilter(typeof(OwnershipValidation))]
        public async Task<IActionResult> Delete(string id)
        {
            var contestId = await this.questionsService.DeleteAsync(id);

            if (contestId == null)
            {
                return this.NotFound();
            }

            return this.NoContent();
        }
    }
}
