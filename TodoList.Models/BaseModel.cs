using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateCreated { get; private set; }= DateTime.Now;
        public bool IsDeleted { get; set; }= false;
    }
}
