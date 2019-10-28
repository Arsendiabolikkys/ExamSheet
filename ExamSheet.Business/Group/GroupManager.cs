using ExamSheet.Repository;
using ExamSheet.Repository.Group;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Business.Group
{
    public class GroupManager : BaseManager<GroupModel>
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

        public override GroupModel GetById(string id)
        {
            return CreateModel(Repository.GetById(id));
        }

        public override GroupModel CreateModel(IEntity entity)
        {
            var group = entity as Repository.Group.Group;
            var model = new GroupModel();
            model.Id = group.Id;
            model.Name = group.Name;
            return model;
        }
    }
}
