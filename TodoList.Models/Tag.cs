﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class Tag:BaseModel
    {
        public string Name { get; set; }

        public virtual ICollection<ToDo>? ToDos { get; set; }=new List<ToDo>();
    }
}
