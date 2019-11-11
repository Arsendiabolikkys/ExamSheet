using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Business
{
    public interface IItemManager<T> where T :IItem
    {
        IEnumerable<T> FindAll();
        T GetById(string id);
        void Save(T model);
        void Remove(string id);
    }
}
