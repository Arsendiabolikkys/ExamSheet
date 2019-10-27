using ExamSheet.Repository;
using System.Collections.Generic;

namespace ExamSheet.Business
{
    public abstract class BaseManager<T> where T : class
    {
        protected RepositoryWrapper repositoryWrapper;

        public BaseManager(RepositoryWrapper repositoryWrapper)
        {
            this.repositoryWrapper = repositoryWrapper;
        }

        public abstract IEnumerable<T> FindAll();
    }
}
