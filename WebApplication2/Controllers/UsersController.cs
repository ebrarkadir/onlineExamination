﻿using Microsoft.AspNetCore.Mvc;
using OnlineExamination.BLL.Services.Abstract;
using OnlineExamination.ViewModels;

namespace OnlineExamination.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IAccountService _accountService;

        public UsersController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_accountService.GetAllTeachers(pageNumber,pageSize));
        }
        [HttpPost]
        public IActionResult Create(UserViewModel userViewModel) 
        {
            if (ModelState.IsValid)
            {
                _accountService.AddTeacher(userViewModel);
                return RedirectToAction("Index");
            }
            return View(userViewModel);
        }
    }
}
