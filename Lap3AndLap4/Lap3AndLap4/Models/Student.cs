using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Lap3AndLap4.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bắt buộc nhập tên")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Bắt buộc nhập Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string? Email { get; set; }

        [StringLength(100,MinimumLength = 8)]
        [Required]
        public string? Password { get; set; }

        [Required]
        public Branch? Branch { get; set; }
        [Required]
        public Gender? Gender { get; set; }
        public bool IsRegular { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string? Address { get; set; }

        [Range(typeof(DateTime), "1/1/1936", "12/31/2005")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateOfBirth { get; set; }

    }
}
