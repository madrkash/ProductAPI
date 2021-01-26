using FluentValidation;
using ProductStore.API.Dtos;

namespace ProductStore.API.Validators
{
    public class ProductCreateRequestValidator : AbstractValidator<ProductCreateRequestDto>
    {
        public ProductCreateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.DeliveryPrice).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}