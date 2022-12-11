using Microsoft.AspNetCore.Mvc;
using quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quiz.Controllers
{
    public class QuizController : Controller
    {
        private readonly Context _context;

        public QuizController(Context context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {

            ViewBag.Tests = _context.Tests.Where(x => x.UserId == id).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Index(string Name)
        {
            ViewBag.Tests = _context.Tests.ToList();


            return View();
        }

    }
}
