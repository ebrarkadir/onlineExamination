using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineExamination.BLL.Services.Abstract;
using OnlineExamination.ViewModels;

namespace OnlineExamination.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IExamService _examService;
        private readonly IQnAService _qnAService;

        public StudentController(IStudentService studentService
            ,IExamService examService, IQnAService qnAService)
        {
            _studentService = studentService;
            _examService = examService;
            _qnAService = qnAService;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_studentService.GetAll(pageNumber, pageSize));
        }
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentWiewModel studentWiewModel)
        {
            if (ModelState.IsValid)
            {
                await _studentService.AddAsync(studentWiewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(studentWiewModel);
        }
        public IActionResult AttendExam()
        {
            var model = new AttendExamViewModel();
            LoginViewModel sessionObj = HttpContext.
                Session.Get<LoginViewModel>("loginvm");
            if (sessionObj != null)
            {
                model.StudentId = Convert.ToInt32(sessionObj.Id);
                model.QnAs = new List<QnAsViewModel>();
                var todayExam = _examService.GetAllExams()
                    .Where(a=>a.StartDate.Date == DateTime.Today.Date).FirstOrDefault();
                if (todayExam==null)
                {
                    model.Message = "No Exam Scheduled Today";
                }
                else
                {
                    if (!_qnAService.IsExamAttended(todayExam.Id, model.StudentId))
                    {
                        model.QnAs = _qnAService.GetAllQnAByExam(todayExam.Id).ToList();
                        model.ExamName = todayExam.Title;
                        model.Message = "";
                    }
                    else
                    {
                        model.Message = "You Have Already Attend This Exam. ";
                    }
                }
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }
    }
}
