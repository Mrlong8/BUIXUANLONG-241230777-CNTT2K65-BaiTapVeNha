using Lap3AndLap4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

public class StudentController : Controller
{
    private static List<Student> listStudents = new List<Student>()
    {
        new Student
        {
            Id = 1,
            Name = "Nguyen Van An",
            Email = "annv@gmail.com",
            Password = "Password@123",
            Branch = Branch.IT,
            Gender = Gender.Male,
            IsRegular = true,
            Address = "Hanoi",
            DateOfBirth = new DateTime(2003, 5, 15),
        },
        new Student
        {
            Id = 2,
            Name = "Tran Thi Mai",
            Email = "maitt@gmail.com",
            Password = "Password@456",
            Branch = Branch.BE,
            Gender = Gender.Female,
            IsRegular = false,
            Address = "Nam Dinh",
            DateOfBirth = new DateTime(2004, 8, 20),
        }
    };

    [HttpGet]
    public IActionResult Index()
    {
        return View(listStudents);
    }

    // 2. Hàm Get để hiển thị form Create
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
        ViewBag.AllBranches = new List<SelectListItem>()
        {
            new SelectListItem { Text = "IT", Value = "1" },
            new SelectListItem { Text = "BE", Value = "2" },
            new SelectListItem { Text = "CE", Value = "3" },
            new SelectListItem { Text = "EE", Value = "4" }
        };
        return View();
    }

    // 3. Hàm Post nhận dữ liệu từ form Create và thêm vào danh sách
    [HttpPost]
    public IActionResult Create(Student s)
    {
        if (ModelState.IsValid)
        {
            s.Id = listStudents.Count > 0 ? listStudents.Last<Student>().Id + 1 : 1;
            listStudents.Add(s);

            return View("Index", listStudents);
        }

        ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
        ViewBag.AllBranches = new List<SelectListItem>()
        {
            new SelectListItem { Text = "IT", Value = "1" },
            new SelectListItem { Text = "BE", Value = "2" },
            new SelectListItem { Text = "CE", Value = "3" },
            new SelectListItem { Text = "EE", Value = "4" }
        };

        return View(s);
    }
}