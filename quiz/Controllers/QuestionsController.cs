using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using quiz.Models;

namespace quiz.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly Context _context;

        public QuestionsController(Context context)
        {

            _context = context;

        }

        public IActionResult Index(int TestId, int ChoiceId, int Page, int CurrentScore, List<int> Answer)
        {

            var test = _context.Tests.Where(x => x.TestID == TestId);
            var pages = test.FirstOrDefault().CountQuestions;

            List<Questions> li = _context.Questions.Where(x => x.TestId == TestId).ToList();

            ViewBag.CurrentPage = Page + 1;
            ViewBag.TestId = TestId;
            ViewBag.Pages = pages;
            ViewBag.Name = li[Page];

            ViewBag.Choices = _context.Choices.Where(x => x.QuestionId == li[Page].QuestionID).ToList();
            ViewBag.Result = 0;

            if (ChoiceId != 0)
                if (_context.Choices.Where(x => x.ChoiceId == ChoiceId).Single().Is_Correct)
                    ViewBag.Result = CurrentScore + 1;
                else
                    ViewBag.Result = CurrentScore;

            return View();

        }

        public IActionResult Results(int TestId, int ChoiceId, int CurrentScore, List<int> Answer)
        {

            var questions = _context.Questions.Where(x => x.TestId == TestId).ToList();

            Dictionary<int, List<Choices>> dictionary = new Dictionary<int, List<Choices>>();

            foreach (var question in questions)
            {

                dictionary.Add(question.QuestionID, _context.Choices.Where(x => x.QuestionId == question.QuestionID).ToList());

            }

            ViewBag.TestId = TestId;
            ViewBag.Questions = questions;
            ViewBag.Choices = dictionary;
            ViewBag.ChoiceId = ChoiceId;
            ViewBag.Result = 0;

            if (_context.Choices.Where(x => x.ChoiceId == ChoiceId).Single().Is_Correct)
                ViewBag.Result = CurrentScore + 1;
            else
                ViewBag.Result = CurrentScore;

            return View();

        }

    }
}
