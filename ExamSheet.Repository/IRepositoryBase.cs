using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Repository
{
    public interface IRepositoryBase<T>
    {
        IEnumerable<T> FindAll();
    }
}