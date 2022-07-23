using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public class PaginationViewModel
    {
        public PageViewModel PageViewModel { get; set; }
        public List<Todo> Todos { get; set; }
    }
}