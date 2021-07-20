using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portal.Domain.Specifications
{
    public static class MaterialSpecification
    {
        public static Specification<Material> UserId(Guid id)
        {
            return new Specification<Material>(x => x.UserMaterials.Any(y => y.UserId == id));
        }

        public static Specification<Material> MaterialId(Guid id)
        {
            return new Specification<Material>(x => x.Id == id);
        }
    }
}
