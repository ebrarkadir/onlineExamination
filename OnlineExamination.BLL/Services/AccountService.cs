using Microsoft.Extensions.Logging;
using OnlineExamination.DataAccess;
using OnlineExamination.DataAccess.UnitOfWork;
using OnlineExamination.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.BLL.Services
{
    public class AccountService : IAccountService
    {
        IUnitOfWork _unitWork;
        ILogger<StudentService> _iLogger;
        public AccountService(IUnitOfWork unitWork, ILogger<StudentService> iLogger)
        {
            _unitWork = unitWork;
            _iLogger = iLogger;
        }

        public bool AddTeacher(UserViewModel vm)
        {
            try
            {
                Users obj = new Users()
                {
                    Name = vm.Name,
                    UserName = vm.UserName,
                    Password = vm.Password,
                    Role = (int)EnumRoles.Teacher
                };
                _unitWork.GenericRepository<Users>().AddAsync(obj);
                _unitWork.Save();
            }
            catch (Exception ex)
            {

                _iLogger.LogError(ex.Message);
                return false;
            }
            return true;
        }

        public PagedResult<UserViewModel> GetAllTeachers(int pageNumber, int pageSize)
        {
            var model = new UserViewModel();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                List<UserViewModel> detailList = new List<UserViewModel>();
                var modelList = _unitWork.GenericRepository<Users>().GetAll()
                    .Where(x=>x.Role == (int)EnumRoles.Teacher).Skip(ExcludeRecords)
                    .Take(pageSize).ToList();
                detailList = ListInfo(modelList);
                if (detailList != null)
                {
                    model.UserList = detailList;
                    model.TotalCount = _unitWork.GenericRepository<Users>().GetAll()
                        .Count(x=>x.Role==(int)EnumRoles.Teacher);
                }
            }
            catch (Exception ex)
            {
                _iLogger.LogError(ex.Message);
            }
            var results = new PagedResult<UserViewModel>
            {
                Data = model.UserList,
                TotalItems = model.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return results;
        }

        private List<UserViewModel> ListInfo(List<Users> modelList)
        {
            return modelList.Select(o => new UserViewModel(o)).ToList();
        }

        public LoginViewModel Login(LoginViewModel vm)
        {
            if (vm.Role == (int)EnumRoles.Admin || vm.Role ==(int)EnumRoles.Teacher)
            {
                var user = _unitWork.GenericRepository<Users>().GetAll()
                    .FirstOrDefault(a => a.UserName == vm.UserName.Trim()
                    && a.Password == vm.Password.Trim() && a.Role == vm.Role);
                if (user != null)
                {
                    vm.Id = user.Id;
                    return vm;
                }
            }
            else
            {
                var student = _unitWork.GenericRepository<Students>().GetAll()
                    .FirstOrDefault(a=>a.UserName == vm.UserName.Trim()
                    && a.Password == vm.Password.Trim());
                if (student != null)
                {
                    vm.Id = student.Id;
                }
                return vm;
            }
            return null;
        }
    }
}
