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
        public AccountService()
        {
            
        }
        public bool AddTeacher(UserViewModel vm)
        {
            throw new NotImplementedException();
        }

        public PagedResult<UserViewModel> GetAllTeachers(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
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
