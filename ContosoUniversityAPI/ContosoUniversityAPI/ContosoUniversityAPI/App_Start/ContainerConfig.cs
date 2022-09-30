using Autofac;
using ContosoUniversity.APIControllers;
using ContosoUniversityAPI.Services;
using System;
using Autofac.Integration.WebApi;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using IContainer = Autofac.IContainer;
using ContosoUniversityAPI.Controllers;

namespace ContosoUniversityAPI.App_Start
{
    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<InstructorServ>().As<IInstructorServ>();
            builder.RegisterApiControllers(typeof(InstructorController).Assembly);
            builder.RegisterType<StudentServ>().As<IStudentServ>();
            builder.RegisterApiControllers(typeof(StudentController).Assembly);
            builder.RegisterType<OfficeAssignmentServ>().As<IOfficeAssignmentServ>();
            builder.RegisterApiControllers(typeof(OfficeAssignmentController).Assembly);
            builder.RegisterType<DepartmentServ>().As<IDepartmentServ>();
            builder.RegisterApiControllers(typeof(DepartmentController).Assembly);
            builder.RegisterType<CourseServ>().As<ICourseServ>();
            builder.RegisterApiControllers(typeof(CourseController).Assembly);
            return builder.Build();
        }
    }
}