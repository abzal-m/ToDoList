using System.Linq;
using ToDoList.Enums;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public class SortViewModel
    {
        public SortState IdSort { get; set; }
        public SortState TitleSort { get; set; }
        public SortState PrioritySort { get; set; }
        public SortState StatusSort { get; set; }
        public SortState CreateDateSort { get; set; }

        public SortViewModel(SortState sortOrder)
        {
            IdSort = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            TitleSort = sortOrder == SortState.TitleAsc ? SortState.TitleDesc : SortState.TitleAsc;
            PrioritySort = sortOrder == SortState.PriorityAsc ? SortState.PriorityDesc : SortState.PriorityAsc;
            StatusSort = sortOrder == SortState.StatusAsc ? SortState.StatusDesc : SortState.StatusAsc;
            CreateDateSort = sortOrder == SortState.CreateDateAsc ? SortState.CreateDateDesc : SortState.CreateDateAsc;
        }
        
    }
}