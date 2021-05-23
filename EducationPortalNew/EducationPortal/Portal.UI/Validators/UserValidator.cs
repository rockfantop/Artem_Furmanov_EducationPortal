using FluentValidation;
using Portal.Application.ModelsDTO;
using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Portal.UI.Validators
{
    class UserValidator : AbstractValidator<InputUserDTO>
    {
        public UserValidator()
        {
            RuleSet("Email", () =>
            {
                RuleFor(x => x.Email).Must(IsValidEmail).WithMessage("Invalid email!");
            });

            RuleSet("Password", () =>
            {
                RuleFor(x => x.Password).Must(IsValidPassword).WithMessage("Invalid password!");
            });
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
