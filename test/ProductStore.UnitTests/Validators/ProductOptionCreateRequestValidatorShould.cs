using FluentValidation.TestHelper;
using ProductStore.API.Validators;
using System;
using ProductStore.API.Dtos;
using Xunit;

namespace ProductStore.UnitTests.Validators
{
    public class ProductOptionCreateRequestValidatorShould
    {
        private readonly ProductOptionCreateRequestValidator _validator = new ProductOptionCreateRequestValidator();

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Have_Error_When_Name_Is_Invalid(string invalidName)
        {
            _validator.ShouldHaveValidationErrorFor(productOptionCreateRequest => productOptionCreateRequest.Name, invalidName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Have_Error_When_Description_Is_Invalid(string invalidDescription)
        {
            _validator.ShouldHaveValidationErrorFor(productOptionCreateRequest => productOptionCreateRequest.Description, invalidDescription);
        }

        [Fact]
        public void Have_Error_When_ProductId_Is_Not_Provided()
        {
            var productOptionCreateRequest = new ProductOptionCreateRequestDto
            {
                Description = "Testing",
                Name = "Test"
            };

            var result = _validator.TestValidate(productOptionCreateRequest);

            Assert.Equal(1, result.Errors.Count);
        }

        [Fact]
        public void Have_Error_When_ProductId_Is_Empty()
        {
            var productOptionCreateRequest = new ProductOptionCreateRequestDto
            {
                Description = "Testing",
                Name = "Test",
                ProductId = Guid.Empty
            };

            var result = _validator.TestValidate(productOptionCreateRequest);

            Assert.Equal(1, result.Errors.Count);
        }

        [Fact]
        public void Not_Have_Error_When_Request_Is_Valid()
        {
            var productOptionCreateRequest = new ProductOptionCreateRequestDto
            {
                Description = "Newest mobile product from Samsung",
                Name = "Samsung Galaxy S10",
                ProductId = Guid.NewGuid()
            };

            var result = _validator.TestValidate(productOptionCreateRequest);
            Assert.True(result.IsValid);
        }
    }
}