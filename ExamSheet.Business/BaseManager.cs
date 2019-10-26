using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Business
{
    public abstract class BaseManager<T> where T : class
    {
        protected IRepositoryWrapper repositoryWrapper;

        public BaseManager(IRepositoryWrapper repositoryWrapper)
        {
            this.repositoryWrapper = repositoryWrapper;
        }
    }
}
