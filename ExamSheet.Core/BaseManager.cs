using ExamSheet.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Core
{
    public abstract class BaseManager<T>
    {
        protected IRepositoryWrapper repositoryWrapper;

        protected IRepositoryBase<T> repository;

        public BaseManager(IRepositoryWrapper repositoryWrapper)
        {
            this.repositoryWrapper = repositoryWrapper;
        }

        public abstract IRepositoryBase<T> GetRepository();
    }
}
