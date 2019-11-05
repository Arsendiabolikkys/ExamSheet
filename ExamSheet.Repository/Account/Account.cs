using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Repository.Account
{
    public class Account : IEntity
    {
        public virtual string Id { get; set; }

        public virtual string Email { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual string Salt { get; set; }

        public virtual string RoleId { get; set; }

        public virtual string ReferenceId { get; set; }
    }
}
