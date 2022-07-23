using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public class FilterViewModel
    {
        public string SelectedTitle { get; private set; } 
        public string SelectedPriority { get; private set; }
        public string SelectedStatus { get; private set; }
        public string SelectedFromDate { get; set; }
        public string SelectedToDate { get; set; }
        public FilterViewModel( string title, string priority, string status, string fromDate, string toDate)
        {
            SelectedTitle = title;
            SelectedPriority = priority;
            SelectedStatus = status;
            SelectedFromDate = fromDate;
            SelectedToDate = toDate;
        }
        
    }
}