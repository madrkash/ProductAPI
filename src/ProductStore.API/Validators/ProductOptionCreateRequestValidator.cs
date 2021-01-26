using FluentValidation;
using System;
using ProductStore.API.Dtos;

namespace ProductStore.API.Validators
{
    public class ProductOptionCreateRequestValidator : AbstractValidator<ProductOptionCreateRequestDto>
    {
        public ProductOptionCreateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ProductId).NotNull().NotEqual(Guid.Empty);
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}