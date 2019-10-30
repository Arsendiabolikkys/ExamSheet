using ExamSheet.Business.Faculty;
using ExamSheet.Business.Group;
using ExamSheet.Business.Semester;
using ExamSheet.Business.Subject;
using ExamSheet.Business.Teacher;
using ExamSheet.Repository;
using ExamSheet.Repository.ExamSheet;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Business.ExamSheet
{
    public class ExamSheetManager : BaseManager<ExamSheetModel>
    {
        protected virtual GroupManager GroupManager { get; set; }

        protected virtual TeacherManager TeacherManager { get; set; }

        protected virtual FacultyManager FacultyManager { get; set; }

        protected virtual SemesterManager SemesterManager { get; set; }

        protected virtual SubjectManager SubjectManager { get; set; }
        
        protected ExamSheetRepository Repository => repositoryWrapper.ExamSheet;

        public ExamSheetManager(RepositoryWrapper repositoryWrapper, GroupManager groupManager, TeacherManager teacherManager,
            FacultyManager facultyManager, SemesterManager semesterManager, SubjectManager subjectManager)
            : base(repositoryWrapper)
        {
            GroupManager = groupManager;
            TeacherManager = teacherManager;
            FacultyManager = facultyManager;
            SemesterManager = semesterManager;
            SubjectManager = subjectManager;
        }

        public override IEnumerable<ExamSheetModel> FindAll()
        {
            return Repository.FindAll().Select(CreateModel);
        }

        public override ExamSheetModel GetById(string id)
        {
            return CreateModel(Repository.GetById(id));
        }

        public override ExamSheetModel CreateModel(IEntity entity)
        {
            var examSheet = entity as Repository.ExamSheet.ExamSheet;
            var model = new ExamSheetModel();
            //model.Id = examSheet.Id;
            model.OpenDate = examSheet.OpenDate;
            model.CloseDate = examSheet.CloseDate;
            model.Ratings = examSheet.Ratings;
            model.State = (ExamSheetState)examSheet.State;
            model.Group = GroupManager.GetById(examSheet.GroupId);
            model.Teacher = TeacherManager.GetById(examSheet.TeacherId);
            model.Faculty = FacultyManager.GetById(examSheet.FacultyId);
            model.Semester = SemesterManager.GetById(examSheet.SemesterId);
            model.Subject = SubjectManager.GetById(examSheet.SubjectId);
            return model;
        }
    }
}