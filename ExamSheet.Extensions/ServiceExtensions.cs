using ExamSheet.Business.ExamSheet;
using ExamSheet.Core.ExamSheet;
using ExamSheet.Data;
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
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureManagers(this IServiceCollection services)
        {
            services.AddTransient<IExamSheetManager, ExamSheetManager>();
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
