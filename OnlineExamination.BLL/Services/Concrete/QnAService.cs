using Microsoft.Extensions.Logging;
using OnlineExamination.BLL.Services.Abstract;
using OnlineExamination.DataAccess;
using OnlineExamination.DataAccess.UnitOfWork;
using OnlineExamination.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.BLL.Services.Concrete
{

    public class QnAService : IQnAService
    {
        IUnitOfWork _unitOfWork;
        ILogger<QnAService> _ilogger;

        public QnAService(IUnitOfWork unitOfWork, ILogger<QnAService> ilogger)
        {
            _unitOfWork = unitOfWork;
            _ilogger = ilogger;
        }

        public async Task<QnAsViewModel> AddSync(QnAsViewModel QnAVM)
        {
            try
            {
                QnAs objQnA = QnAVM.ConvertViewModel(QnAVM);
                await _unitOfWork.GenericRepository<QnAs>().AddAsync(objQnA);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                return null;
            }
            return QnAVM;
        }

        public PagedResult<QnAsViewModel> GetAll(int pageNumber, int pageSize)
        {

            var model = new QnAsViewModel();
            try
            {
                int ExcludeRecords = pageSize * pageNumber - pageSize;
                List<QnAsViewModel> detailList = new List<QnAsViewModel>();
                var modelList = _unitOfWork.GenericRepository<QnAs>().GetAll().Skip(ExcludeRecords)
                    .Take(pageSize).ToList();
                var totalCount = _unitOfWork.GenericRepository<QnAs>().GetAll().ToList();
                detailList = QnAsListInfo(modelList);
                if (detailList != null)
                {
                    model.QnAsList = detailList;
                    model.TotalCount = totalCount.Count();
                }
            }
            catch (Exception ex)
            {

                _ilogger.LogError(ex.Message);
            }
            var result = new PagedResult<QnAsViewModel>
            {
                Data = model.QnAsList,
                TotalItems = model.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        private List<QnAsViewModel> QnAsListInfo(List<QnAs> modelList)
        {
            return modelList.Select(o => new QnAsViewModel(o)).ToList();
        }

        public IEnumerable<QnAsViewModel> GetAllQnAByExam(int examId)
        {
            try
            {
                var qnaList = _unitOfWork.GenericRepository<QnAs>().GetAll()
                    .Where(x => x.ExamsId == examId);
                return QnAsListInfo(qnaList.ToList());

            }
            catch (Exception ex)
            {

                _ilogger.LogError(ex.Message);
            }
            return Enumerable.Empty<QnAsViewModel>();
        }

        public bool IsExamAttended(int examId, int studentId)
        {
            try
            {
                var qnaRecord = _unitOfWork.GenericRepository<ExamResults>().GetAll()
                    .FirstOrDefault(x => x.ExamsId == examId && x.StudentsId == studentId);
                return qnaRecord == null ? false : true;
            }
            catch (Exception ex)
            {

                _ilogger.LogError(ex.Message);
            }
            return false;
        }

    }
}
