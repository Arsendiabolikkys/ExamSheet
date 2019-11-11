using ExamSheet.Business.Role;

namespace ExamSheet.Business.Account
{
    public class AccountModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public AccountType AccountType { get; set; }
        //public RoleModel Role { get; set; }

        public string ReferenceId { get; set; }
    }

    public enum AccountType
    {
        Teacher,
        Deanery,
        Admin
    }
}
