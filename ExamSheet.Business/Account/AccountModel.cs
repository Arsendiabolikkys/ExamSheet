
namespace ExamSheet.Business.Account
{
    public class AccountModel : IItem
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public AccountType AccountType { get; set; }

        public string ReferenceId { get; set; }
    }

    public enum AccountType
    {
        Teacher,
        Deanery,
        Admin
    }
}
