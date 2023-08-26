using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
        [HttpPost]
        public IActionResult AttendExam(AttendExamViewModel attendExamViewModel)
        {
            bool result = _studentService.SetExamResult(attendExamViewModel);
            return RedirectToAction("AttendExam");
        }
        public IActionResult Result(string studentId)
        {
            var model = _studentService.GetExamResults(Convert.ToInt32(studentId));
            return View(model);
        }
        public IActionResult ViewResult()
        {
            LoginViewModel sessionObj = HttpContext.Session.Get<LoginViewModel>("loginvm");
            if (sessionObj!=null)
            {
                var model = _studentService.GetExamResults(Convert.ToInt32(sessionObj.Id));
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }
        public IActionResult Profile()
        {
            LoginViewModel sessionObj = HttpContext.Session.Get<LoginViewModel>("loginvm");
            if (sessionObj != null)
            {
                var model = _studentService.GetStudentDetails(Convert.ToInt32(sessionObj.Id));
                if (model.PictureFileName != null)
                {
                    model.PictureFileName = ConfigurationManager.GetFilePath() + model.PictureFileName;
                }
                model.CVFileName = ConfigurationManager.GetFilePath() + model.CVFileName;
                return View(model);
            }
            return RedirectToAction("Login", "Account");
           
        }
        public IActionResult Profile([FromForm]StudentWiewModel studentWiewModel)
        {
            if (studentWiewModel.PictureFile!=null)
                studentWiewModel.PictureFileName = SaveStudentFile(studentWiewModel.PictureFile);
            if (studentWiewModel.CVFile != null)
            studentWiewModel.CVFileName = SaveStudentFile(studentWiewModel.CVFile);
            _studentService.UpdateAsync(studentWiewModel);
            return RedirectToAction("Profile");
        }

        private string SaveStudentFile(IFormFile pictureFile)
        {
            if (pictureFile==null)
            {
                return string.Empty;
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/file");
            return SaveFile(path, pictureFile);
        }

        private string SaveFile(string path, IFormFile pictureFile)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var filename = Guid.NewGuid().ToString() + "." + pictureFile.FileName.Split('.')
                [pictureFile.FileName.Split('.').Length - 1];
            path = Path.Combine(path, filename);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                pictureFile.CopyTo(stream);
            }
            return filename;
        }
    }
}
