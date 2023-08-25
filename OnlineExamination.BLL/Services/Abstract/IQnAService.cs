using OnlineExamination.DataAccess;
using OnlineExamination.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.BLL.Services.Abstract
{
    public interface IQnAService
    {
        PagedResult<QnAsViewModel> GetAll(int pageNumber, int pageSize);
        Task<QnAsViewModel> AddSync(QnAsViewModel QnAVM);
        IEnumerable<QnAsViewModel> GetAllQnAByExam(int examId);
        bool IsExamAttended(int examId, int studentId);
    }
}
