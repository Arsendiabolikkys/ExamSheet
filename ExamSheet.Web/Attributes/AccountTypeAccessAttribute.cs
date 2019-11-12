using ExamSheet.Business.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExamSheet.Web.Attributes
{
    public class IsInRoleAttribute : TypeFilterAttribute
    {
        public IsInRoleAttribute(AccountType accountType) : base(typeof(AccountTypeFilter))
        {
            Arguments = new object[] { accountType };
        }
    }

    public class AccountTypeFilter : IAuthorizationFilter
    {
        readonly AccountType _accountType;
        
        public AccountTypeFilter(AccountType accountType)
        {
            _accountType = accountType;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated || !context.HttpContext.User.IsInRole(_accountType.ToString()))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}