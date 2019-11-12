using ExamSheet.Repository.Account;
using ExamSheet.Repository.Deanery;
using ExamSheet.Repository.ExamSheet;
using ExamSheet.Repository.Faculty;
using ExamSheet.Repository.Group;
using ExamSheet.Repository.Rating;
using ExamSheet.Repository.Semester;
using ExamSheet.Repository.Student;
using ExamSheet.Repository.Subject;
using ExamSheet.Repository.Teacher;
using NHibernate;

namespace ExamSheet.Repository
{
    public class RepositoryWrapper
    {
        private ISessionFactory sessionFactory;
        private ExamSheetRepository examSheet;
        private GroupRepository group;
        private TeacherRepository teacher;
        private SubjectRepository subject;
        private DeaneryRepository deanery;
        private FacultyRepository faculty;
        private SemesterRepository semester;
        private StudentRepository student;
        private RatingRepository rating;
        private AccountRepository account;

        public AccountRepository Account
        {
            get
            {
                if (account == null)
                {
                    account = new AccountRepository(sessionFactory);
                }
                return account;
            }
        }

        public ExamSheetRepository ExamSheet
        {
            get
            {
                if (examSheet == null)
                {
                    examSheet = new ExamSheetRepository(sessionFactory);
                }
                return examSheet;
            }
        }

        public GroupRepository Group
        {
            get
            {
                if (group == null)
                {
                    group = new GroupRepository(sessionFactory);
                }
                return group;
            }
        }

        public TeacherRepository Teacher
        {
            get
            {
                if (teacher == null)
                {
                    teacher = new TeacherRepository(sessionFactory);
                }
                return teacher;
            }
        }

        public SubjectRepository Subject
        {
            get
            {
                if (subject == null)
                {
                    subject = new SubjectRepository(sessionFactory);
                }
                return subject;
            }
        }

        public DeaneryRepository Deanery
        {
            get
            {
                if (deanery == null)
                {
                    deanery = new DeaneryRepository(sessionFactory);
                }
                return deanery;
            }
        }

        public FacultyRepository Faculty
        {
            get
            {
                if (faculty == null)
                {
                    faculty = new FacultyRepository(sessionFactory);
                }
                return faculty;
            }
        }

        public RatingRepository Rating
        {
            get
            {
                if (rating == null)
                {
                    rating = new RatingRepository(sessionFactory);
                }
                return rating;
            }
        }

        public SemesterRepository Semester
        {
            get
            {
                if (semester == null)
                {
                    semester = new SemesterRepository(sessionFactory);
                }
                return semester;
            }
        }

        public StudentRepository Student
        {
            get
            {
                if (student == null)
                {
                    student = new StudentRepository(sessionFactory);
                }
                return student;
            }
        }

        public RepositoryWrapper(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }
    }
}
