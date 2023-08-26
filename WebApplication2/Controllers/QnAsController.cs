using Microsoft.AspNetCore.Mvc;
using OnlineExamination.BLL.Services.Abstract;
using OnlineExamination.ViewModels;

namespace OnlineExamination.Web.Controllers
{
    public class QnAsController : Controller
    {
        private readonly IExamService _examService;
        private readonly IQnAService _qnAService;

        public QnAsController(IExamService examService, IQnAService qnAService)
        {
            _examService = examService;
            _qnAService = qnAService;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_qnAService.GetAll(pageNumber, pageSize));
        }
        public IActionResult Create()
        {
            var model = new QnAsViewModel();
            model.ExamList = _examService.GetAllExams();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(QnAsViewModel qnAsViewModel)
        {
            if (ModelState.IsValid)
            {
                await _qnAService.AddSync(qnAsViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(qnAsViewModel);
        }
    }
}
