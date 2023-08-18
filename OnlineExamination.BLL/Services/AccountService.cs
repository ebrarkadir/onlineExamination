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
            throw new NotImplementedException();
        }
    }
}
