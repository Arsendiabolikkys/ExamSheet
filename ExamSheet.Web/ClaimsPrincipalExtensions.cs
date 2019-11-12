using ExamSheet.Business.Account;
using System.Security.Claims;

namespace ExamSheet.Web
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsInRole(this ClaimsPrincipal user, AccountType accountType)
        {
            return user.IsInRole(accountType.ToString());
        }
    }
}
