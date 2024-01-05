using FluentValidation;
using HolwnEcommerce.Admin.ProductCategories;
using System;
using System.Collections.Generic;
using System.Text;

namespace HolwnEcommerce.Admin.Roles
{
    public class CreateUpdateRoleDtoValidator : AbstractValidator<CreateUpdateRoleDto>
    {
        public CreateUpdateRoleDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
