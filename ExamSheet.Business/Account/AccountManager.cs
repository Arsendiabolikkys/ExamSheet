using System.Collections.Generic;
using System.Linq;
using ExamSheet.Business.Role;
using ExamSheet.Repository;
using ExamSheet.Repository.Account;

namespace ExamSheet.Business.Account
{
    public class AccountManager : BaseManager<AccountModel>
    {
        protected virtual RoleManager RoleManager { get; set; }

        public AccountManager(RepositoryWrapper repositoryWrapper, RoleManager roleManager)
            : base(repositoryWrapper)
        {
            RoleManager = roleManager;
        }

        protected AccountRepository Repository => repositoryWrapper.Account;

        public override AccountModel CreateModel(IEntity entity)
        {
            var account = entity as Repository.Account.Account;
            if (account == null)
                return null;
            var model = new AccountModel();
            model.Id = account.Id;
            model.Email = account.Email;
            model.PasswordHash = account.PasswordHash;
            model.ReferenceId = account.ReferenceId;
            model.Role = RoleManager.GetById(account.RoleId);
            model.Salt = account.Salt;
            return model;
        }

        public override IEnumerable<AccountModel> FindAll()
        {
            return Repository.FindAll().Select(CreateModel);
        }

        public override AccountModel GetById(string id)
        {
            var account = Repository.GetById(id);
            if (account == null)
                return null;
            return CreateModel(account);
        }
    }
}
