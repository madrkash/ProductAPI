using FluentValidation;
using ProductStore.API.ApiModels;
using System;

namespace ProductStore.API.Validators
{
    public class ProductOptionCreateRequestValidator : AbstractValidator<ProductOptionCreateRequest>
    {
        public ProductOptionCreateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ProductId).NotNull().NotEqual(Guid.Empty);
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}