﻿using ExamSheet.Repository;
using ExamSheet.Repository.Group;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Business.Group
{
    public class GroupManager : BaseManager<GroupModel>, IItemManager<GroupModel>
    {
        public GroupManager(RepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper)
        {
        }

        protected GroupRepository Repository => repositoryWrapper.Group;

        public override IEnumerable<GroupModel> FindAll()
        {
            return Repository.FindAll().Select(CreateModel);
        }

        public virtual IEnumerable<GroupModel> FindAll(int page, int count)
        {
            return Repository.FindAll(page, count).Select(CreateModel);
        }

        public virtual IEnumerable<GroupModel> FindAll(string facultyId, int page, int count)
        {
            if (string.IsNullOrEmpty(facultyId))
                return FindAll(page, count);

            return Repository.FindAll(facultyId, page, count).Select(CreateModel);
        }

        public virtual int GetTotal()
        {
            return Repository.GetTotal();
        }

        public virtual int GetTotal(string facultyId)
        {
            if (string.IsNullOrEmpty(facultyId))
                return GetTotal();
            return Repository.GetTotal(facultyId);
        }

        public virtual IEnumerable<GroupModel> FindAllForFaculty(string facultyId)
        {
            return Repository.FindAllForFaculty(facultyId).Select(CreateModel);
        }

        public override GroupModel GetById(string id)
        {
            var group = Repository.GetById(id);
            if (group == null)
                return null;
            return CreateModel(group);
        }

        public virtual List<GroupModel> GetByIdList(IEnumerable<string> ids)
        {
            if (!ids?.Any() ?? true)
                return new List<GroupModel>();

            var groups = Repository.GetByIdList(ids.ToArray());
            if (!groups?.Any() ?? true)
                return new List<GroupModel>();

            return groups.Select(CreateModel).ToList();
        }

        public virtual void Save(GroupModel groupModel)
        {
            if (groupModel == null)
                return;
            if (string.IsNullOrEmpty(groupModel.Id))
                return;
            Repository.Save(CreateModel(groupModel));
        }

        public virtual void Remove(string id)
        {
            if (string.IsNullOrEmpty(id))
                return;

            Repository.Remove(id);
        }

        public virtual Repository.Group.Group CreateModel(GroupModel groupModel)
        {
            var group = new Repository.Group.Group();
            group.Id = groupModel.Id;
            group.Name = groupModel.Name;
            group.FacultyId = groupModel.FacultyId;
            return group;
        }

        public override GroupModel CreateModel(IEntity entity)
        {
            var group = entity as Repository.Group.Group;
            var model = new GroupModel();
            model.Id = group.Id;
            model.Name = group.Name;
            model.FacultyId = group.FacultyId;
            return model;
        }
    }
}
