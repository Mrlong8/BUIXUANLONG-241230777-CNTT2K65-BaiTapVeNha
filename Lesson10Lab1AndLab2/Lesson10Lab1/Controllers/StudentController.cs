using Lesson10Lab1.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lesson10Lab1.Controllers
{
    public class StudentController : Controller
    {
        // Bắt buộc phải có static để dữ liệu không bị mất/reset khi bấm Create
        private static List<Student> list = new List<Student>()
        {
            new Student(){ Id = 101, Name = "Hai Nam", Branch = Branch.IT, Gender = Gender.Male, IsRegular = true, Address = "A1-2022", Email = "nam@gmail.com" },
            new Student(){ Id = 102, Name = "Minh Anh", Branch = Branch.BE, Gender = Gender.Female, IsRegular = false, Address = "B2-2023", Email = "anh@gmail.com" },
            new Student(){ Id = 102, Name = "Minh Anh", Branch = Branch.BE, Gender = Gender.Female, IsRegular = false, Address = "B2-2023", Email = "anh@gmail.com" },
            new Student(){ Id = 102, Name = "Minh Anh", Branch = Branch.BE, Gender = Gender.Female, IsRegular = false, Address = "B2-2023", Email = "anh@gmail.com" },
            new Student(){ Id = 102, Name = "Minh Anh", Branch = Branch.BE, Gender = Gender.Female, IsRegular = false, Address = "B2-2023", Email = "anh@gmail.com" },
            new Student(){ Id = 102, Name = "Minh Anh", Branch = Branch.BE, Gender = Gender.Female, IsRegular = false, Address = "B2-2023", Email = "anh@gmail.com" }
        };

        [Route("Admin/Student/List", Name = "Student-list")]
        public IActionResult Index()
        {
            return View(list);
        }

        [HttpGet]
        [Route("Admin/Student/Add", Name = "student_add")]
        public IActionResult Create()
        {
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();

            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem{ Text = "IT", Value = "IT" },
                new SelectListItem{ Text = "BE", Value = "BE" },
                new SelectListItem{ Text = "CE", Value = "CE" },
                new SelectListItem{ Text = "EE", Value = "EE" }
            };
            return View();
        }

        [HttpPost]
        [Route("Admin/Student/Add")]
        public IActionResult Create(Student s)
        {
            s.Id = list.Last<Student>().Id + 1;

            list.Add(s);

            return RedirectToAction("Index");
        }
    }
}


//< a href = "/Admin/Student/Add" class= "btn btn-primary mb-3" > Create student </ a >
//< a asp - controller = "Student" asp - action = "Create" class= "btn btn-primary mb-3" > Create student </ a >
//< a asp - route = "student_add" class= "btn btn-primary mb-3" > Create student </ a >
//< form action = "/Admin/Student/Add" method = "post" >
//< form asp - route = "student_add" method = "post" >