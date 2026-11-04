using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Lap3AndLap4.Models
{
    [Table("Enrollment")]
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public virtual Course Course { get; set; }
        public int LearnerID { get; set; }
        public virtual Learner Learner { get; set; }
        public float? Grade { get; set; }
    }
}