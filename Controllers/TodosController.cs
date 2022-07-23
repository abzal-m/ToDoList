using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;
using ToDoList.Enums;
using ToDoList.ViewModels;

namespace ToDoList.Controllers
{
    public class TodosController : Controller
    {
        private readonly TodoContext _db;

        public TodosController(TodoContext db)
        {
            _db = db;
        }

        // GET
        public async Task<IActionResult>  Index(string title, string priority, string status, string fromDate, string toDate, int page = 1, SortState sortOrder = SortState.IdAsc)
        {
            int pageSize = 3;
            IQueryable<Todo> todos = _db.Todos;
            // фильтарция
            if (!string.IsNullOrEmpty(title))
            {
                todos = todos.Where(p => p.Title.Contains(title));
            }
            if (!string.IsNullOrEmpty(priority) && priority != "All")
            {
                todos = todos.Where(p => p.Priority.Contains(priority));
            }
            if (!string.IsNullOrEmpty(status) && status != "All")
            {
                todos = todos.Where(p => p.Status.Contains(status));
            }
            if (!string.IsNullOrEmpty(fromDate))
            {
                var isDate = DateTime.TryParse(fromDate, out DateTime date);
                if (isDate)
                {
                    todos = todos.Where(a => a.CreateDate >= date);
                }
            }
            
            if (!string.IsNullOrEmpty(toDate))
            {
                var isDate = DateTime.TryParse(toDate, out DateTime date);
                if (isDate)
                {
                    todos = todos.Where(a => date >= a.CreateDate);
                }
            }
            
            // сортировка
            
            ViewBag.IdSort = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            ViewBag.TitleSort = sortOrder == SortState.TitleAsc ? SortState.TitleDesc : SortState.TitleAsc;
            ViewBag.PrioritySort = sortOrder == SortState.PriorityAsc ? SortState.PriorityDesc : SortState.PriorityAsc;
            ViewBag.StatusSort = sortOrder == SortState.StatusAsc ? SortState.StatusDesc : SortState.StatusAsc;
            ViewBag.CreateDateSort =
                sortOrder == SortState.CreateDateAsc ? SortState.CreateDateDesc : SortState.CreateDateAsc;

            switch (sortOrder)
            {
                case SortState.IdDesc:
                    todos = todos.OrderByDescending(t => t.Id);
                    break;
                case SortState.TitleAsc:
                    todos = todos.OrderBy(t => t.Title);
                    break;
                case SortState.TitleDesc:
                    todos = todos.OrderByDescending(t => t.Title);
                    break;
                case SortState.PriorityAsc:
                    todos = todos.OrderBy(t => t.Priority);
                    break;
                case SortState.PriorityDesc:
                    todos = todos.OrderByDescending(t => t.Priority);
                    break;
                case SortState.StatusAsc:
                    todos = todos.OrderBy(t => t.Status);
                    break;
                case SortState.StatusDesc:
                    todos = todos.OrderByDescending(t => t.Status);
                    break;
                case SortState.CreateDateAsc:
                    todos = todos.OrderBy(t => t.CreateDate);
                    break;
                case SortState.CreateDateDesc:
                    todos = todos.OrderByDescending(t => t.CreateDate);
                    break;
            }
            // пагинация
            var count = await todos.CountAsync();
            var items = await todos.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
           
            
            
            TodoListViewModel viewModel = new TodoListViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(title, priority, status, fromDate, toDate),
                Todos = items
            };
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Todo todo)
        {
            if (ModelState.IsValid)
            {
                todo.Status = "Новая";
                todo.CreateDate = DateTime.Now;
                _db.Todos.Add(todo);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult GetTodo(int id)
        {
            Todo todo = _db.Todos.FirstOrDefault(d => d.Id == id);
            return View(todo);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Todo todo = _db.Todos.Find(id);
            if (todo.Status == TaskStatuses.Новая.ToString() || todo.Status == TaskStatuses.Закрыта.ToString())
            {
                _db.Todos.Remove(todo);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult TodoOpen(int id)
        {
            Todo todo = _db.Todos.Find(id);
            if (todo.Status == TaskStatuses.Новая.ToString())
            {
                todo.Status = TaskStatuses.Открыта.ToString();
                todo.OpenDate = DateTime.Now;
                _db.Todos.Update(todo);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult TodoClose(int id)
        {
            Todo todo = _db.Todos.Find(id);
            if (todo.Status == TaskStatuses.Открыта.ToString())
            {
                todo.Status = TaskStatuses.Закрыта.ToString();
                todo.CloseDate = DateTime.Now;
                _db.Todos.Update(todo);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        
    }
}