using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using quiz.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace quiz.Controllers
{

    public class HomeController : Controller
    {
        private readonly Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            if (HttpContext.Session.GetString("JWToken") != null)
            {
                var user = HttpContext.Session.GetString("JWToken");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(user);
                var tokenS = jsonToken as JwtSecurityToken;
                var value = tokenS.Payload.FirstOrDefault(x => x.Key == "USERID").Value;
                var id = _context.User.Where(x => x.Login == value).Single().USERID;
                return Redirect("~/Quiz/Index/" + id);
            }
            else
            {
                return View();
            }

        }

        public IActionResult LoginUser(User user)
        {

            var listUser = _context.User.SingleOrDefault(x => x.Login == user.Login);
            string userToken = null;
            if (listUser == null && user.Login != null)
            {

                User user1 = new User
                {
                    NAME = user.Login,
                    Login = user.Login
                };
                _context.Add(user1);
                var key = Encoding.ASCII.GetBytes("YourKey-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv");
                var JWToken = new JwtSecurityToken(
                    issuer: "http://localhost:44366/",
                    audience: "http://localhost:44366/",
                    claims: GetUserClaims(user1),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                );
                userToken = new JwtSecurityTokenHandler().WriteToken(JWToken);

                _context.SaveChanges();

                listUser = _context.User.SingleOrDefault(x => x.Login == user.Login);

                Tests tests1 = new Tests()
                {
                    Name = "Quiz 1",
                    Description = "A quiz about how good the weather today",
                    CountQuestions = 1,
                    UserId = listUser.USERID
                };

                _context.Add(tests1);
                _context.SaveChanges();

                Questions question1 = new Questions()
                {
                    Question = "Is the weather good today?",
                    TestId = tests1.TestID
                };

                _context.Add(question1);

                _context.SaveChanges();

                Choices choice1 = new Choices()
                {
                    Choice = "Yes",
                    Is_Correct = false,
                    QuestionId = question1.QuestionID
                };

                _context.Add(choice1);

                Choices choice2 = new Choices()
                {
                    Choice = "No",
                    Is_Correct = false,
                    QuestionId = question1.QuestionID
                };

                _context.Add(choice2);

                Choices choice3 = new Choices()
                {
                    Choice = "50/50",
                    Is_Correct = false,
                    QuestionId = question1.QuestionID
                };

                _context.Add(choice3);

                Choices choice4 = new Choices()
                {
                    Choice = "very cold",
                    Is_Correct = true,
                    QuestionId = question1.QuestionID
                };

                _context.Add(choice4);

                _context.SaveChanges();

                Tests tests2 = new Tests()
                {
                    Name = "Quiz 2",
                    Description = "Quiz about sport",
                    CountQuestions = 2,
                    UserId = listUser.USERID
                };

                _context.Add(tests2);
                _context.SaveChanges();

                Questions question2 = new Questions()
                {
                    Question = "In which country is football called: 'Soccer'?",
                    TestId = tests2.TestID
                };

                _context.Add(question2);

                _context.SaveChanges();

                Choices choice1_2 = new Choices()
                {
                    Choice = "Mexico",
                    Is_Correct = false,
                    QuestionId = question2.QuestionID
                };

                _context.Add(choice1_2);

                Choices choice2_2 = new Choices()
                {
                    Choice = "England",
                    Is_Correct = false,
                    QuestionId = question2.QuestionID
                };

                _context.Add(choice2_2);

                Choices choice3_2 = new Choices()
                {
                    Choice = "USA",
                    Is_Correct = true,
                    QuestionId = question2.QuestionID
                };

                _context.Add(choice3_2);

                Choices choice4_2 = new Choices()
                {
                    Choice = "Ukraine",
                    Is_Correct = false,
                    QuestionId = question2.QuestionID
                };

                _context.Add(choice4_2);

                _context.SaveChanges();

                Questions question2_2 = new Questions()
                {
                    Question = "The best basketball league:",
                    TestId = tests2.TestID
                };

                _context.Add(question2_2);

                _context.SaveChanges();

                Choices choice1_3 = new Choices()
                {
                    Choice = "EuroLeague",
                    Is_Correct = false,
                    QuestionId = question2_2.QuestionID
                };

                _context.Add(choice1_3);

                Choices choice2_3 = new Choices()
                {
                    Choice = "NBA",
                    Is_Correct = true,
                    QuestionId = question2_2.QuestionID
                };

                _context.Add(choice2_3);

                Choices choice3_3 = new Choices()
                {
                    Choice = "Spain's Liga ACB",
                    Is_Correct = false,
                    QuestionId = question2_2.QuestionID
                };

                _context.Add(choice3_3);

                Choices choice4_3 = new Choices()
                {
                    Choice = "Turkish Basketball Super League",
                    Is_Correct = false,
                    QuestionId = question2_2.QuestionID
                };

                _context.Add(choice4_3);

                _context.SaveChanges();

                _context.SaveChanges();
                if (userToken != null)
                {
                    HttpContext.Session.SetString("JWToken", userToken);
                }

                return Redirect("~/Quiz/Index/" + listUser.USERID);

            }
            else if (user.Login != null)
            {

                var key = Encoding.ASCII.GetBytes("YourKey-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv");
                var JWToken = new JwtSecurityToken(
                    issuer: "http://localhost:44366/",
                    audience: "http://localhost:44366/",
                    claims: GetUserClaims(listUser),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                );
                userToken = new JwtSecurityTokenHandler().WriteToken(JWToken);

                _context.SaveChanges();
                if (userToken != null)
                {
                    HttpContext.Session.SetString("JWToken", userToken);
                }

                return Redirect("~/Quiz/Index/" + listUser.USERID);

            }

            else
            {
                return Redirect("~/Home/Index/");
            }


        }

        private IEnumerable<Claim> GetUserClaims(User user)
        {

            IEnumerable<Claim> claims = new Claim[]
                    {
                new Claim(ClaimTypes.Name, user.NAME),
                new Claim("USERID", user.Login),
                    };
            return claims;

        }

        public IActionResult Logoff()
        {

            HttpContext.Session.Clear();
            return Redirect("~/Home/Index");

        }
    }
}
