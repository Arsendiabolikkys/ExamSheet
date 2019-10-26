using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Data
{
    public interface IRepositoryBase<T>
    {
        IEnumerable<T> FindAll();
    }
}