using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Business
{
    public interface IRepositoryBase<T>
    {
        IEnumerable<T> FindAll();
    }
}