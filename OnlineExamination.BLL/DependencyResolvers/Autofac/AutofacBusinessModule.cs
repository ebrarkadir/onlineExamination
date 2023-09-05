using Autofac;
using OnlineExamination.BLL.Services.Abstract;
using OnlineExamination.BLL.Services.Concrete;
using OnlineExamination.DataAccess.Context;
using OnlineExamination.DataAccess.Repository;
using OnlineExamination.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.BLL.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<onlineExamDbContext>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance();
            builder.RegisterType<GroupService>().As<IGroupService>().SingleInstance();

            builder.RegisterType<StudentService>().As<IStudentService>().SingleInstance();
            builder.RegisterType<ExamService>().As<IExamService>().SingleInstance();

            builder.RegisterType<QnAService>().As<IQnAService>().SingleInstance();
            builder.RegisterType<AccountService>().As<IAccountService>().SingleInstance();

        }
    }
}
