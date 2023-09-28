using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;
using TodoList.Repository.Shared.Abstract;

namespace TodoList.Repository.Abstract
{
    public interface ITodoRepositorty:IRepository<ToDo>
    {
        void SetIsActive(int id);
    }
}
