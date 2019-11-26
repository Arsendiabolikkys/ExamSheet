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

        public virtual IEnumerable<ExamSheetModel> FindAll(int page, int pageSize)
        {
            return Repository.FindAll(page, pageSize).Select(CreateModel);
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

        public virtual List<ExamSheetModel> Get(string facultyId, string subjectId)
        {
            if (string.IsNullOrEmpty(facultyId) || string.IsNullOrEmpty(subjectId))
                return new List<ExamSheetModel>();

            var sheets = Repository.Get(facultyId, subjectId);
            if (!sheets?.Any() ?? true)
                return new List<ExamSheetModel>();

            return sheets.Select(CreateModel).ToList();
        }

        public IEnumerable<ExamSheetModel> FindClosedForTeacher(string teacherId)
        {
            if (string.IsNullOrEmpty(teacherId))
                return new List<ExamSheetModel>();

            return Repository.FindForTeacher(teacherId, (short)ExamSheetState.Closed).Select(CreateModel).ToList();
        }

        public IEnumerable<ExamSheetModel> FindClosedForFaculty(string facultyId)
        {
            if (string.IsNullOrEmpty(facultyId))
                return new List<ExamSheetModel>();

            return Repository.FindForFaculty(facultyId, (short)ExamSheetState.Closed).Select(CreateModel).ToList();
        }

        public IEnumerable<ExamSheetModel> FindAllForTeacher(string teacherId, int page, int pageSize)
        {
            if (string.IsNullOrEmpty(teacherId))
                return new List<ExamSheetModel>();

            return Repository.FindAllForTeacher(teacherId, page, pageSize).Select(CreateModel);
        }

        public IEnumerable<ExamSheetModel> FindAll(SheetFilter filter, int page, int pageSize)
        {
            if (filter == null)
                return FindAll(page, pageSize);
            
            return Repository.FindAll(CreateFilter(filter), page, pageSize).Select(CreateModel);
        }

        protected virtual Dictionary<string, object> CreateFilter(SheetFilter filter)
        {
            if (filter == null)
                return new Dictionary<string, object>();
            var repositoryFilter = new Dictionary<string, object>();
            repositoryFilter.Add(nameof(SheetFilter.State), filter.State);
            repositoryFilter.Add(nameof(SheetFilter.FacultyId), filter.FacultyId);
            repositoryFilter.Add(nameof(SheetFilter.GroupId), filter.GroupId);
            repositoryFilter.Add(nameof(SheetFilter.SubjectId), filter.SubjectId);
            repositoryFilter.Add(nameof(SheetFilter.TeacherId), filter.TeacherId);
            return repositoryFilter;
        }

        public IEnumerable<ExamSheetModel> FindAllForFaculty(string facultyId, int page, int pageSize)
        {
            if (string.IsNullOrEmpty(facultyId))
                return new List<ExamSheetModel>();

            return Repository.FindAllForFaculty(facultyId, page, pageSize).Select(CreateModel);
        }

        public virtual int GetTotal()
        {
            return Repository.GetTotal();
        }

        public virtual int GetTotalForTeacher(string teacherId)
        {
            if (string.IsNullOrEmpty(teacherId))
                return 0;
            return Repository.GetTotalForTeacher(teacherId);
        }

        public virtual int GetTotal(SheetFilter filter)
        {
            return Repository.GetTotal(CreateFilter(filter));
        }

        public virtual int GetTotalForFaculty(string facultyId)
        {
            if (string.IsNullOrEmpty(facultyId))
                return 0;
            return Repository.GetTotalForFaculty(facultyId);
        }

        public override ExamSheetModel GetById(string id)
        {
            return CreateModel(Repository.GetById(id));
        }

        public virtual bool CloseSheet(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;
            var sheet = Repository.GetById(id);
            if (sheet == null)
                return false;

            sheet.State = (short)ExamSheetState.Closed;
            sheet.CloseDate = DateTime.Now;
            Repository.Save(sheet);
            return true;
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
            //examSheet.OpenDate = examSheetModel.OpenDate;
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
            //model.OpenDate = examSheet.OpenDate;
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