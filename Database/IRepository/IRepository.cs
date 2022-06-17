using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.IRepository
{
    public interface IRepository<T>
    {
        List<T> List();
        T Get(int id);
        void Add(T item);
        void Delete(int id);
        bool Update(T item, int id);
    }
}
