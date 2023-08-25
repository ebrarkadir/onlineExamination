using OnlineExamination.DataAccess;
using OnlineExamination.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.BLL.Services.Abstract
{
    public interface IStudentService
    {
        PagedResult<StudentWiewModel> GetAll(int pageNumber, int pageSize);
        Task<StudentWiewModel> AddAsync(StudentWiewModel vm);
        IEnumerable<Students> GetAllStudents();
        bool SetGroupIdToStudents(GroupViewModel vm);
        bool SetExamResult(AttendExamViewModel vm);
        IEnumerable<ResultViewModel> GetExamResults(int studentId);
        StudentWiewModel GetStudentDetails(int studentId);
        Task<StudentWiewModel> UpdateAsync(StudentWiewModel vm);

    }
}
