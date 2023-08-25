using Microsoft.Extensions.Logging;
using OnlineExamination.BLL.Services.Abstract;
using OnlineExamination.DataAccess;
using OnlineExamination.DataAccess.UnitOfWork;
using OnlineExamination.ViewModels;

namespace OnlineExamination.BLL.Services.Concrete
{
    public class StudentService : IStudentService
    {
        IUnitOfWork _unitOfWork;
        ILogger<StudentService> _ilogger;

        public StudentService(IUnitOfWork unitOfWork, ILogger<StudentService> ilogger)
        {
            _unitOfWork = unitOfWork;
            _ilogger = ilogger;
        }

        public async Task<StudentWiewModel> AddAsync(StudentWiewModel vm)
        {
            try
            {
                Students obj = vm.ConvertViewModel(vm);
                await _unitOfWork.GenericRepository<Students>().AddAsync(obj);
            }
            catch (Exception ex)
            {
                return null;
            }
            return vm;
        }

        public PagedResult<StudentWiewModel> GetAll(int pageNumber, int pageSize)
        {
            var model = new StudentWiewModel();
            try
            {
                int ExcludeRecords = pageSize * pageNumber - pageSize;
                List<StudentWiewModel> detailList = new List<StudentWiewModel>();
                var modelList = _unitOfWork.GenericRepository<Students>().GetAll()
                    .Skip(ExcludeRecords).Take(pageSize).ToList();
                var totalCount = _unitOfWork.GenericRepository<Students>().GetAll().ToList();
                detailList = GroupListInfo(modelList);
                if (detailList != null)
                {
                    model.StudentList = detailList;
                    model.TotalCount = totalCount.Count();
                }
            }
            catch (Exception ex)
            {

                _ilogger.LogError(ex.Message);
            }
            var result = new PagedResult<StudentWiewModel>
            {
                Data = model.StudentList,
                TotalItems = model.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        private List<StudentWiewModel> GroupListInfo(List<Students> modelList)
        {
            return modelList.Select(o => new StudentWiewModel(o)).ToList();
        }

        public IEnumerable<Students>? GetAllStudents()
        {
            try
            {
                var students = _unitOfWork.GenericRepository<Students>().GetAll();
                return students;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
            }
            return Enumerable.Empty<Students>();
        }

        public IEnumerable<ResultViewModel> GetExamResults(int studentId)
        {
            try
            {
                var examResults = _unitOfWork.GenericRepository<ExamResults>().GetAll()
                    .Where(a => a.StudentsId == studentId);
                var students = _unitOfWork.GenericRepository<Students>().GetAll();
                var exams = _unitOfWork.GenericRepository<ExamResults>().GetAll();
                var qnas = _unitOfWork.GenericRepository<QnAs>().GetAll();

                var requiredData = examResults.Join(students, er => er.StudentsId, s => s.Id,
                    (er, st) => new { er, st }).Join(exams, erj => erj.er.ExamsId, ex => ex.Id,
                    (erj, ex) => new { erj, ex }).Join(qnas, exj => exj.erj.er.QnAsId, q => q.Id,
                    (exj, q) => new ResultViewModel()
                    {
                        StudentId = studentId,
                        ExamName = exj.ex.Title,
                        TotalQuestion = examResults.Count(a => a.StudentsId == studentId
                        && a.ExamsId == exj.ex.Id),
                        CorrectAnswer = examResults.Count(a => a.StudentsId == studentId
                        && a.ExamsId == exj.ex.Id && a.Answer == q.Answer),
                        WrongAnswer = examResults.Count(a => a.StudentsId == studentId
                        && a.ExamsId == exj.ex.Id && a.Answer != q.Answer)
                    });
                return requiredData;
            }
            catch (Exception ex)
            {

                _ilogger.LogError(ex.Message);
            }
            return Enumerable.Empty<ResultViewModel>();
        }

        public StudentWiewModel GetStudentDetails(int studentId)
        {
            try
            {
                var student = _unitOfWork.GenericRepository<Students>().GetByID(studentId);
                return student != null ? new StudentWiewModel(student) : null;
            }
            catch (Exception ex)
            {

                _ilogger.LogError(ex.Message);
            }
            return null;
        }

        public bool SetExamResult(AttendExamViewModel vm)
        {
            try
            {
                foreach (var item in vm.QnAs)
                {
                    ExamResults examResaults = new ExamResults();
                    examResaults.StudentsId = vm.StudentId;
                    examResaults.QnAsId = item.Id;
                    examResaults.ExamsId = item.ExamsId;
                    examResaults.Answer = item.SelectedAnswer;
                    _unitOfWork.GenericRepository<ExamResults>().AddAsync(examResaults);
                }
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);

            }
            return false;
        }

        public bool SetGroupIdToStudents(GroupViewModel vm)
        {
            try
            {
                foreach (var item in vm.StudentCheckList)
                {
                    var student = _unitOfWork.GenericRepository<Students>().GetByID(item.Id);
                    if (item.Selected)
                    {
                        student.GroupsId = vm.Id;
                        _unitOfWork.GenericRepository<Students>().Update(student);
                    }
                    else
                    {
                        if (student.GroupsId == vm.Id)
                        {
                            student.GroupsId = null;
                        }
                    }
                    _unitOfWork.Save();
                    return true;
                }
            }
            catch (Exception ex)
            {

                _ilogger.LogError(ex.Message);
            }
            return false;
        }

        public async Task<StudentWiewModel> UpdateAsync(StudentWiewModel vm)
        {
            try
            {
                Students obj = _unitOfWork.GenericRepository<Students>().GetByID(vm.Id);
                obj.Name = vm.Name;
                obj.UserName = vm.UserName;
                obj.PictureFileName = vm.PictureFileName != null ?
                    vm.PictureFileName : obj.PictureFileName;
                obj.CVFileName = vm.CVFileName != null ?
                    vm.CVFileName : obj.CVFileName;
                await _unitOfWork.GenericRepository<Students>().UpdateAsync(obj);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {

                _ilogger.LogError(ex.Message);
            }
            return vm;
        }
    }
}
