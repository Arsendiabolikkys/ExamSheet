using ExamSheet.Business.Faculty;
using ExamSheet.Business.Group;
using ExamSheet.Business.Rating;
using ExamSheet.Business.Subject;
using ExamSheet.Business.Teacher;
using ExamSheet.Repository;
using ExamSheet.Repository.ExamSheet;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Business.ExamSheet
{
    public class ExamSheetManager : BaseManager<ExamSheetModel>
    {
        protected virtual GroupManager GroupManager { get; set; }

        protected virtual TeacherManager TeacherManager { get; set; }

        protected virtual FacultyManager FacultyManager { get; set; }

        protected virtual SubjectManager SubjectManager { get; set; }

        protected virtual RatingManager RatingManager { get; set; }
        
        protected ExamSheetRepository Repository => repositoryWrapper.ExamSheet;

        public ExamSheetManager(RepositoryWrapper repositoryWrapper, GroupManager groupManager, TeacherManager teacherManager,
            FacultyManager facultyManager, SubjectManager subjectManager, RatingManager ratingManager)
            : base(repositoryWrapper)
        {
            GroupManager = groupManager;
            TeacherManager = teacherManager;
            FacultyManager = facultyManager;
            SubjectManager = subjectManager;
            RatingManager = ratingManager;
        }

        public override IEnumerable<ExamSheetModel> FindAll()
        {
            return Repository.FindAll().Select(CreateModel);
        }

        public virtual ExamSheetModel Get(string groupId, string teacherId, string subjectId, short year, short semester)
        {
            if (string.IsNullOrEmpty(groupId) || string.IsNullOrEmpty(teacherId) || string.IsNullOrEmpty(subjectId))
                return null;
            var sheet = Repository.Get(groupId, teacherId, subjectId, year, semester);
            if (sheet == null)
                return null;

            return CreateModel(sheet);
        }

        public IEnumerable<ExamSheetModel> FindAllForTeacher(string teacherId)
        {
            if (string.IsNullOrEmpty(teacherId))
                return new List<ExamSheetModel>();

            return Repository.FindAllForTeacher(teacherId).Select(CreateModel).Where(x => x.State == ExamSheetState.Open || x.State == ExamSheetState.Closed);
        }

        public IEnumerable<ExamSheetModel> FindAllForFaculty(string facultyId)
        {
            if (string.IsNullOrEmpty(facultyId))
                return new List<ExamSheetModel>();

            return Repository.FindAllForFaculty(facultyId).Select(CreateModel);
        }

        public override ExamSheetModel GetById(string id)
        {
            return CreateModel(Repository.GetById(id));
        }

        public virtual void Save(ExamSheetModel model)
        {
            if (model == null)
                return;
            if (string.IsNullOrEmpty(model.Id))
                return;
            Repository.Save(CreateModel(model));
        }

        public virtual void Remove(string id)
        {
            if (string.IsNullOrEmpty(id))
                return;

            Repository.Remove(id);
        }

        public virtual Repository.ExamSheet.ExamSheet CreateModel(ExamSheetModel examSheetModel)
        {
            var examSheet = new Repository.ExamSheet.ExamSheet();
            examSheet.Id = examSheetModel.Id;
            examSheet.FacultyId = examSheetModel.FacultyId;
            examSheet.GroupId = examSheetModel.GroupId;
            examSheet.OpenDate = examSheetModel.OpenDate;
            examSheet.State = (short)examSheetModel.State;
            examSheet.SubjectId = examSheetModel.SubjectId;
            examSheet.TeacherId = examSheetModel.TeacherId;
            examSheet.CloseDate = examSheetModel.CloseDate;
            examSheet.Semester = examSheetModel.Semester;
            examSheet.Year = examSheetModel.Year;
            return examSheet;
        }

        public override ExamSheetModel CreateModel(IEntity entity)
        {
            var examSheet = entity as Repository.ExamSheet.ExamSheet;
            var model = new ExamSheetModel();
            model.Id = examSheet.Id;
            model.OpenDate = examSheet.OpenDate;
            model.CloseDate = examSheet.CloseDate;
            model.FacultyId = examSheet.FacultyId;
            model.GroupId = examSheet.GroupId;
            model.Semester = examSheet.Semester;
            model.SubjectId = examSheet.SubjectId;
            model.TeacherId = examSheet.TeacherId;
            model.Year = examSheet.Year;
            model.Ratings = RatingManager.FindAll(examSheet.Id);
            if ((ExamSheetState)examSheet.State == ExamSheetState.New && examSheet.OpenDate.HasValue && examSheet.OpenDate.Value >= DateTime.Now)
                model.State = ExamSheetState.Open;
            else
                model.State = (ExamSheetState)examSheet.State;

            return model;
        }
    }
}