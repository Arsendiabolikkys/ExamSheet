using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Business
{
    public interface IItemManager<T> where T :IItem
    {
        IEnumerable<T> FindAll();
        IEnumerable<T> FindAll(int page, int pageSize);
        int GetTotal();
        T GetById(string id);
        void Save(T model);
        void Remove(string id);
    }
}
