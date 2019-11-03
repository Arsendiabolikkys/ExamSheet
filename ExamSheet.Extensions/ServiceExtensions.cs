using ExamSheet.Business.Deanery;
using ExamSheet.Business.ExamSheet;
using ExamSheet.Business.Faculty;
using ExamSheet.Business.Group;
using ExamSheet.Business.Rating;
using ExamSheet.Business.Semester;
using ExamSheet.Business.Student;
using ExamSheet.Business.Subject;
using ExamSheet.Business.Teacher;
using ExamSheet.Repository;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.NetCore;
using System;
using System.IO;

namespace ExamSheet.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddSingleton<RepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureManagers(this IServiceCollection services)
        {
            services.AddTransient<ExamSheetManager, ExamSheetManager>();
            services.AddTransient<GroupManager, GroupManager>();
            services.AddTransient<FacultyManager, FacultyManager>();
            services.AddTransient<TeacherManager, TeacherManager>();
            services.AddTransient<SubjectManager, SubjectManager>();
            services.AddTransient<StudentManager, StudentManager>();
            services.AddTransient<SemesterManager, SemesterManager>();
            services.AddTransient<DeaneryManager, DeaneryManager>();
            services.AddTransient<RatingManager, RatingManager>();
        }

        public static void UseAsHibernateFactory(this Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            loggerFactory.UseAsHibernateLoggerFactory();
        }

        public static void ConfigureHibernate(this IServiceCollection services)
        {
            var path = Path.Combine(
              AppDomain.CurrentDomain.BaseDirectory,
              "hibernate.config.xml"
             );
            services.AddHibernate(path);
        }
    }
}
