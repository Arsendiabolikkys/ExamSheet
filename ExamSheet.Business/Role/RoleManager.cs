using System;
using System.Collections.Generic;
using System.Linq;
using ExamSheet.Repository;
using ExamSheet.Repository.Role;

namespace ExamSheet.Business.Role
{
    public class RoleManager : BaseManager<RoleModel>
    {
        public RoleManager(RepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper) { }

        protected RoleRepository Repository => repositoryWrapper.Role;

        public override RoleModel CreateModel(IEntity entity)
        {
            var role = entity as Repository.Role.Role;
            if (role == null)
                return null;
            var model = new RoleModel();
            model.Id = role.Id;
            model.Name = role.Name;
            model.Description = role.Description;
            return model;
        }

        public override IEnumerable<RoleModel> FindAll()
        {
            return Repository.FindAll().Select(CreateModel);
        }

        public override RoleModel GetById(string id)
        {
            var role = Repository.GetById(id);
            if (role == null)
                return null;
            return CreateModel(role);
        }
    }
}
