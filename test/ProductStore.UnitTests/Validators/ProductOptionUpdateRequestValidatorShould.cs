using FluentValidation.TestHelper;
using ProductStore.API.ApiModels;
using ProductStore.API.Validators;
using System;
using Xunit;

namespace ProductStore.UnitTests.Validators
{
    public class ProductOptionUpdateRequestValidatorShould
    {
        private readonly ProductOptionUpdateRequestValidator _validator = new ProductOptionUpdateRequestValidator();

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Have_Error_When_Name_Is_Invalid(string invalidName)
        {
            _validator.ShouldHaveValidationErrorFor(productOptionUpdateRequest => productOptionUpdateRequest.Name, invalidName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Have_Error_When_Description_Is_Invalid(string invalidDescription)
        {
            _validator.ShouldHaveValidationErrorFor(productOptionUpdateRequest => productOptionUpdateRequest.Description, invalidDescription);
        }

        [Fact]
        public void Have_Error_When_Id_ProductId_Are_Not_Provided()
        {
            var productOptionUpdateRequest = new ProductOptionUpdateRequest
            {
                Description = "Testing",
                Name = "Test"
            };

            var result = _validator.TestValidate(productOptionUpdateRequest);

            Assert.Equal(2, result.Errors.Count);
            result.ShouldHaveValidationErrorFor(x => x.ProductId);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Have_Error_When_Id_ProductId_Are_Empty()
        {
            var productOptionUpdateRequest = new ProductOptionUpdateRequest
            {
                Id = Guid.Empty,
                Description = "Testing",
                Name = "Test",
                ProductId = Guid.Empty
            };

            var result = _validator.TestValidate(productOptionUpdateRequest);

            Assert.Equal(2, result.Errors.Count);
            result.ShouldHaveValidationErrorFor(x => x.ProductId);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Not_Have_Error_When_Request_Is_Valid()
        {
            var productOptionUpdateRequest = new ProductOptionUpdateRequest
            {
                Id = Guid.NewGuid(),
                Description = "Test Product Option",
                Name = "Test",
                ProductId = Guid.NewGuid()
            };

            var result = _validator.TestValidate(productOptionUpdateRequest);
            Assert.True(result.IsValid);
        }
    }
}