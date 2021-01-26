using FluentValidation;
using System;
using ProductStore.API.Dtos;

namespace ProductStore.API.Validators
{
    public class ProductOptionUpdateRequestValidator : AbstractValidator<ProductOptionUpdateRequestDto>
    {
        public ProductOptionUpdateRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEqual(Guid.Empty);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ProductId).NotNull().NotEqual(Guid.Empty);
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}