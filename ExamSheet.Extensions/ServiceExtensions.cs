using ExamSheet.Business;
using ExamSheet.Business.ExamSheet;
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
            services.AddScoped<RepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureManagers(this IServiceCollection services)
        {
            services.AddTransient<ExamSheetManager, ExamSheetManager>();
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
