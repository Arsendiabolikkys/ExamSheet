using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Data
{
    public class RepositoryContext
    {
        public ISessionFactory Factory;

        public RepositoryContext(ISessionFactory factory)
        {
            Factory = factory;
        }
    }
}
