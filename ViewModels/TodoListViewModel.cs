using System.Collections;
using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public class TodoListViewModel
    {
        public IEnumerable<Todo> Todos { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}