﻿using Microsoft.Extensions.Logging;
using OnlineExamination.BLL.Services.Abstract;
using OnlineExamination.DataAccess;
using OnlineExamination.DataAccess.UnitOfWork;
using OnlineExamination.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OnlineExamination.BLL.Services.Concrete
{
    public class ExamService : IExamService
    {
        IUnitOfWork _unitOfWork;
        ILogger<ExamService> _ilogger;

        public ExamService(IUnitOfWork unitOfWork, ILogger<ExamService> ilogger)
        {
            _unitOfWork = unitOfWork;
            _ilogger = ilogger;
        }

        public async Task<ExamViewModel> AddSync(ExamViewModel examVM)
        {
            try
            {
                Exams objExam = examVM.ConvertViewModel(examVM);
                await _unitOfWork.GenericRepository<Exams>().AddAsync(objExam);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                return null;
            }
            return examVM;
        }

        public PagedResult<ExamViewModel> GetAll(int pageNumber, int pageSize)
        {
            var model = new ExamViewModel();
            try
            {
                int ExcludeRecords = pageSize * pageNumber - pageSize;
                List<ExamViewModel> detailList = new List<ExamViewModel>();
                var modelList = _unitOfWork.GenericRepository<Exams>().GetAll().Skip(ExcludeRecords)
                    .Take(pageSize).ToList();
                var totalCount = _unitOfWork.GenericRepository<Exams>().GetAll().ToList();
                detailList = ExamListInfo(modelList);
                if (detailList != null)
                {
                    model.ExamList = detailList;
                    model.TotalCount = totalCount.Count();
                }
            }
            catch (Exception ex)
            {

                _ilogger.LogError(ex.Message);
            }
            var result = new PagedResult<ExamViewModel>
            {
                Data = model.ExamList,
                TotalItems = model.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        private List<ExamViewModel> ExamListInfo(List<Exams> modelList)
        {
            return modelList.Select(o => new ExamViewModel(o)).ToList();
        }

        public IEnumerable<Exams> GetAllExams()
        {
            try
            {
                var exams = _unitOfWork.GenericRepository<Exams>().GetAll();
                return exams;
            }
            catch (Exception ex)
            {

                _ilogger.LogError(ex.Message);
            }
            return Enumerable.Empty<Exams>();
        }
    }
}
