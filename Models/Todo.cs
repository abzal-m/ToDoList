using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Todo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите название задачи")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Введите описание")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Приоритет не выбран")]
        public string Priority { get; set; }
#nullable enable
        public string? Status { get; set; }
#nullable enable
        public DateTime? CreateDate { get; set; }
#nullable enable
        public DateTime? OpenDate { get; set; }
#nullable enable
        public DateTime? CloseDate { get; set; }
    }
}