using FluentValidation;
using ProductStore.API.ApiModels;
using System;

namespace ProductStore.API.Validators
{
    public class ProductOptionUpdateRequestValidator : AbstractValidator<ProductOptionUpdateRequest>
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