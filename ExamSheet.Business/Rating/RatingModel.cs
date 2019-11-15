using ExamSheet.Business.Semester;
using ExamSheet.Business.Student;
using ExamSheet.Business.Subject;

namespace ExamSheet.Business.Rating
{
    public class RatingModel
    {
        public string StudentId { get; set; }

        public string ExamSheetId { get; set; }

        public short Mark { get; set; }
    }
}