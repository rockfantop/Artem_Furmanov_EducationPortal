using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Specifications
{
    public static class UserMaterialSpecification
    {
        public static Specification<UserMaterial> UserId(Guid id)
        {
            return new Specification<UserMaterial>(x => x.UserId == id);
        }

        public static Specification<UserMaterial> MaterialId(Guid id)
        {
            return new Specification<UserMaterial>(x => x.MaterialId == id);
        }
    }
}
