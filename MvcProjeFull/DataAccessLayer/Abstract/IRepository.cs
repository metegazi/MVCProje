using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IRepository<T>
    {
        //Burada Methodu oluşturuyoruz.
        List<T> List();

        void Insert(T p);
        void Delete(T p);
        void Update(T p);

        T Get(Expression<Func<T, bool>> filter);
        List<T> List(Expression<Func<T, bool>> filter);

        T Get2(Expression<Func<T, bool>> filter);
    }
}
