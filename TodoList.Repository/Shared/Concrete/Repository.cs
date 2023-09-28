using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TodoList.Data;
using TodoList.Models;
using TodoList.Repository.Shared.Abstract;

namespace TodoList.Repository.Shared.Concrete
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            _dbSet.Update(entity);
        }

        public void DeleteById(int id)
        {
            T entity =_dbSet.Find(id);
            entity.IsDeleted = true;
            _dbSet.Update(entity);

        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.Where(t=>t.IsDeleted==false);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter)
        {
             return GetAll().Where(filter);
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            return _dbSet.FirstOrDefault(filter);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
