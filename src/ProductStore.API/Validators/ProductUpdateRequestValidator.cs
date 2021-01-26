using FluentValidation;
using System;
using ProductStore.API.Dtos;

namespace ProductStore.API.Validators
{
    public class ProductUpdateRequestValidator : AbstractValidator<ProductUpdateRequestDto>
    {
        public ProductUpdateRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEqual(Guid.Empty);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.DeliveryPrice).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}