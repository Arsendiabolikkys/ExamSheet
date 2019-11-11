using System.Collections.Generic;
using System.Linq;
using ExamSheet.Repository;
using ExamSheet.Repository.Account;

namespace ExamSheet.Business.Account
{
    public class AccountManager : BaseManager<AccountModel>, IItemManager<AccountModel>
    {
        public AccountManager(RepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper)
        {
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
            //model.Role = RoleManager.GetById(account.RoleId);
            model.AccountType = (AccountType)account.AccountType;
            model.Salt = account.Salt;
            return model;
        }

        public override IEnumerable<AccountModel> FindAll()
        {
            return Repository.FindAll().Where(x => !x.AccountType.Equals(AccountType.Admin)).Select(CreateModel);
        }

        public override AccountModel GetById(string id)
        {
            var account = Repository.GetById(id);
            if (account == null)
                return null;
            return CreateModel(account);
        }

        public virtual AccountModel GetByEmail(string email)
        {
            var account = Repository.GetByEmail(email);
            return CreateModel(account);
        }

        public virtual void Save(AccountModel accountModel)
        {
            if (accountModel == null)
                return;
            if (string.IsNullOrEmpty(accountModel.Id))
                return;
            Repository.Save(CreateModel(accountModel));
        }

        public virtual Repository.Account.Account CreateModel(AccountModel accountModel)
        {
            var account = new Repository.Account.Account();
            account.Id = accountModel.Id;
            account.Email = accountModel.Email;
            account.AccountType = (short)accountModel.AccountType;
            account.ReferenceId = accountModel.ReferenceId;
            account.PasswordHash = accountModel.PasswordHash;
            account.Salt = accountModel.Salt;
            return account;
        }

        public virtual void Remove(string id)
        {
            if (string.IsNullOrEmpty(id))
                return;

            Repository.Remove(id);
        }
    }
}
