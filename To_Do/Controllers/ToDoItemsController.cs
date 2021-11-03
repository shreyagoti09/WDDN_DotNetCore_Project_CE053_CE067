using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using To_Do.Models;

namespace To_Do.Controllers
{
    public class ToDoItemsController : Controller
    {
        private readonly ToDos _context;
      

        public ToDoItemsController(ToDos context)
        {
            _context = context;
        }

        // GET: ToDoItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.toDoItems.ToListAsync());
        }

        // GET: ToDoItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItems = await _context.toDoItems
                .FirstOrDefaultAsync(m => m.todo_id == id);
            if (toDoItems == null)
            {
                return NotFound();
            }

            return View(toDoItems);
        }

        // GET: ToDoItems/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: ToDoItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("todo_id,task_name,date_added,due_date")] ToDoItems toDoItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toDoItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toDoItems);
        }

        // GET: ToDoItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItems = await _context.toDoItems.FindAsync(id);
            if (toDoItems == null)
            {
                return NotFound();
            }
            return View(toDoItems);
        }

        // POST: ToDoItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("todo_id,task_name,date_added,due_date")] ToDoItems toDoItems)
        {
            if (id != toDoItems.todo_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoItemsExists(toDoItems.todo_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(toDoItems);
        }

        // GET: ToDoItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItems = await _context.toDoItems
                .FirstOrDefaultAsync(m => m.todo_id == id);
            if (toDoItems == null)
            {
                return NotFound();
            }

            return View(toDoItems);
        }

        // POST: ToDoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lastItem = _context.bargraphItems.OrderByDescending(x => x.date).FirstOrDefault();
            //var lastItem = _context.bargraphItems.LastOrDefault<BargraphItems>();
            if(lastItem.date== DateTime.Today)
            {
                lastItem.completed_task += 1;
            }
            else
            {
                var bar = new BargraphItems();
                bar.completed_task = 1;
                bar.date = DateTime.Today;
                _context.bargraphItems.Add(bar);
            }
            var toDoItems = await _context.toDoItems.FindAsync(id);
            _context.toDoItems.Remove(toDoItems);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        private bool DbContextOptions()
        {
            throw new NotImplementedException();
        }

        private bool ToDoItemsExists(int id)
        {
            return _context.toDoItems.Any(e => e.todo_id == id);
        }
    }
}
