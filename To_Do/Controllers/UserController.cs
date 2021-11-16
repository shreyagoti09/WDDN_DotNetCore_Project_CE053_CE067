using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using To_Do.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace To_Do.Controllers
{
    public class UserController : Controller
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            User user = await _context.User.FirstOrDefaultAsync(u => u.user_email == email);
            if (user != null)
            {
                if (String.Equals(user.password.Trim(), password))
                {
                    ViewBag.Message = "";
                    HttpContext.Session.SetInt32("user_id", user.user_id);
                    return RedirectToAction("Index", "ToDoItems");
                }
            }
            ViewBag.Message = "Invalid Email or Password";
            return View();
        }

        public ActionResult Register()
        {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string username, string email, string password, string password1)
        {
            User usr = await _context.User.FirstOrDefaultAsync(u => u.user_email == email);
            if (usr != null)
                ViewBag.Message = "User is already Registered";
            else
            {
                if (password == password1)
                {
                    ViewBag.Message = "";
                    User user = new User();
                    user.username = username;
                    user.user_email = email;
                    user.password = password;
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login", "User");
                }
                else
                    ViewBag.Message = "Confirm password not matched";
            }
            return View();
        }

        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            return View();
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
