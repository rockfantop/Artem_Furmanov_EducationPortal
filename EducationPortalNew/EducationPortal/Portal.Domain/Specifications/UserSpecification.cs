using Portal.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Specifications
{
    public static class UserSpecification
    {
        public static Specification<User> Email(string email)
        {
            return new Specification<User>(x => x.Email == email);
        }

        public static Specification<User> Password(string password)
        {
            return new Specification<User>(x => x.PasswordHash == password);
        }
    }
}
