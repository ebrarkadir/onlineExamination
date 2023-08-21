using Microsoft.Extensions.Logging;
using OnlineExamination.DataAccess;
using OnlineExamination.DataAccess.UnitOfWork;
using OnlineExamination.ViewModels;

namespace OnlineExamination.BLL.Services
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
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                List<StudentWiewModel> detailList =  new List<StudentWiewModel>();
                var modelList = _unitOfWork.GenericRepository<Students>().GetAll()
                    .Skip(ExcludeRecords).Take(pageSize).ToList();
                var totalCount = _unitOfWork.GenericRepository<Students>().GetAll().ToList();
                detailList = GroupListInfo(modelList);
                if (detailList!= null)
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
            throw new NotImplementedException();
        }

        public IEnumerable<ResultViewModel> GetExamResults(int studentId)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public bool SetGroupIdToStudents(GroupViewModel vm)
        {
            throw new NotImplementedException();
        }

        public Task<StudentWiewModel> UpdateAsync(StudentWiewModel vm)
        {
            throw new NotImplementedException();
        }
    }
}
