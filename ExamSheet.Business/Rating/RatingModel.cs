using ExamSheet.Business.Semester;
using ExamSheet.Business.Student;
using ExamSheet.Business.Subject;

namespace ExamSheet.Business.Rating
{
    public class RatingModel
    {
        public StudentModel Student { get; set; }

        public short Mark { get; set; }
    }
}