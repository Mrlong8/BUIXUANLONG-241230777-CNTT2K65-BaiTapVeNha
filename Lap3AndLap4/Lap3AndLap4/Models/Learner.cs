using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Lap3AndLap4.Models {

    [Table("Learner")]
    public class Learner
    {
        [Key]
        public int LearnerID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int MajorID { get; set; }
        public virtual Major Major { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
    }