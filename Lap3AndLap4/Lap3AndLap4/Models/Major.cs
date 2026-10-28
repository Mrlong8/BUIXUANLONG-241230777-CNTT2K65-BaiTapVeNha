using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Major")]
public class Major
{
    public int MajorID { get; set; }
    public string MajorName { get; set; }

    public virtual ICollection<Learner> Learners { get; set; }
}