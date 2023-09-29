using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Data;
using TodoList.Models;
using TodoList.Repository.Abstract;
using TodoList.Repository.Concrete;
using TodoList.Repository.Shared.Abstract;

namespace TodoList.Repository.Shared.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        
        public IRepository<AppUser> AppUsers { get; private set; }

        public IToDoRepository ToDos { get; private set; }

        public IRepository<Category> Categories { get; private set; }

        public IRepository<UserType> UserTypes { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            AppUsers = new Repository<AppUser>(context);
            ToDos = new ToDoRepository(context);
            Categories = new Repository<Category>(context);
            UserTypes = new Repository<UserType>(context);

        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
