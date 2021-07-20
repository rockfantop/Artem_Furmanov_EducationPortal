using FluentValidation;
using Portal.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Portal.WebUI.FluentValidations
{
    public class AccountCreateValidationRules : AbstractValidator<AccountCreateViewModel>
    {
        public AccountCreateValidationRules()
        {
                RuleFor(x => x.Email).Must(IsValidEmail).WithMessage("Invalid email!");

                RuleFor(x => x.Password).Must(IsValidPassword).WithMessage("Invalid password!");
            
                RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords are not equal!");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool IsValidPassword(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            return hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password);
        }
    }
}
