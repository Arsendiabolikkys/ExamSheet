using System.Collections.Generic;
using ExamSheet.Repository;

namespace ExamSheet.Business.Faculty
{
    public class FacultyManager : BaseManager<FacultyModel>
    {
        public FacultyManager(RepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper) { }

        //TODO: virtual in base class? Use FICT FEED MAPPER in base class, otherwise override it
        public override IEnumerable<FacultyModel> FindAll()
        {
            throw new System.NotImplementedException();
        }
    }
}