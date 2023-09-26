using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TodoList.Data;
using TodoList.Repository.Shared.Abstract;

namespace TodoList.Repository.Shared.Concrete
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbset;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public List<T> GetAll()
        {
            return _dbset.ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            return _dbset.Find(id);
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            return _dbset.FirstOrDefault(filter);
        }

        public void Update(T entity)
        {
            _dbset.Update(entity);
        }
    }
}
