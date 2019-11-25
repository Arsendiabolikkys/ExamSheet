using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            return Repository.FindAll().Where(x => !((AccountType)x.AccountType).Equals(AccountType.Admin)).Select(CreateModel);
        }

        public virtual IEnumerable<AccountModel> FindAll(int page, int count)
        {
            return Repository.FindAll(page, count).Select(CreateModel);
        }

        public virtual int GetTotal()
        {
            return Repository.GetTotal();
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

        public bool IsPasswordValid(string email, string password)
        {
            AccountModel account = GetByEmail(email);
            if (account == null)
                return false;
            
            byte[] storedSalt = Convert.FromBase64String(account.Salt);
            byte[] storedPassword = Convert.FromBase64String(account.PasswordHash);
            var cryptedPassword = new Rfc2898DeriveBytes(password, storedSalt);
            var passwordBytes = cryptedPassword.GetBytes(20);
                
            for (int i = 0; i < storedPassword.Length; ++i)
            {
                if (storedPassword[i] != passwordBytes[i])
                    return false;
            }
            return true;
        }
    }
}