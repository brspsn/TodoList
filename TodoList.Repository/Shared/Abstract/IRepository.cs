using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Repository.Shared.Abstract
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> filter);
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        //bu metod link expresini alacakki içine alacağı exprssinin sonucunun true yada false olması lazım,o ifadeden kurtulan birşey varsa T olarak geri gön
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
    }
}
