using ExamSheet.Repository;
using System;
using System.Collections.Generic;

namespace ExamSheet.Business.Group
{
    public class GroupManager : BaseManager<GroupModel>
    {
        public GroupManager(RepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper) { }

        public override IEnumerable<GroupModel> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
