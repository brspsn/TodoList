using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class ToDo:BaseModel
    {
        public string Title { get; set; }
        public string? Desciription { get; set; }
        public bool IsActive { get; set; }=true;
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public virtual Category Category { get; set; }
        public virtual AppUser User { get; set; }

    }
}
