using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Models
{
    [Table("Users")]
    public class AppUser:BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserTypeId { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual ICollection<ToDo> ToDos { get; set; }=new List<ToDo>();

    }
}
