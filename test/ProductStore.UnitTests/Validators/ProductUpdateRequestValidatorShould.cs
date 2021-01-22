using FluentValidation.TestHelper;
using ProductStore.API.ApiModels;
using ProductStore.API.Validators;
using System;
using Xunit;

namespace ProductStore.UnitTests.Validators
{
    public class ProductUpdateRequestValidatorShould
    {
        private readonly ProductUpdateRequestValidator _validator = new ProductUpdateRequestValidator();

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Have_Error_When_Name_Is_Invalid(string invalidName)
        {
            _validator.ShouldHaveValidationErrorFor(productUpdateRequest => productUpdateRequest.Name, invalidName);
        }

        [Fact]
        public void Have_Error_When_Price_Is_Zero()
        {
            _validator.ShouldHaveValidationErrorFor(productUpdateRequest => productUpdateRequest.Price, 0);
        }

        [Fact]
        public void Have_Error_When_DeliveryPrice_Is_Zero()
        {
            _validator.ShouldHaveValidationErrorFor(productUpdateRequest => productUpdateRequest.DeliveryPrice, 0);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Have_Error_When_Description_Is_Invalid(string invalidDescription)
        {
            _validator.ShouldHaveValidationErrorFor(productUpdateRequest => productUpdateRequest.Description, invalidDescription);
        }

        [Fact]
        public void Have_Multiple_Errors_When_Id_Price_DeliveryPrice_Are_Not_Provided()
        {
            var productUpdateRequest = new ProductUpdateRequest
            {
                Description = "Testing",
                Name = "Test"
            };

            var result = _validator.TestValidate(productUpdateRequest);

            Assert.Equal(3, result.Errors.Count);
            result.ShouldHaveValidationErrorFor(request => request.Id);
            result.ShouldHaveValidationErrorFor(request => request.Price);
            result.ShouldHaveValidationErrorFor(request => request.DeliveryPrice);
        }

        [Fact]
        public void Have_Error_When_Id_Is_Empty()
        {
            var productUpdateRequest = new ProductUpdateRequest
            {
                Description = "Testing",
                Name = "Test",
                Id = Guid.Empty,
                Price = 10,
                DeliveryPrice = 1
            };

            var result = _validator.TestValidate(productUpdateRequest);

            Assert.Equal(1, result.Errors.Count);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Not_Have_Error_When_Request_Is_Valid()
        {
            var productUpdateRequest = new ProductUpdateRequest
            {
                Description = "Testing",
                Name = "Test",
                Id = Guid.NewGuid(),
                Price = 10,
                DeliveryPrice = 1
            };

            var result = _validator.TestValidate(productUpdateRequest);
            Assert.Empty(result.Errors);
        }
    }
}