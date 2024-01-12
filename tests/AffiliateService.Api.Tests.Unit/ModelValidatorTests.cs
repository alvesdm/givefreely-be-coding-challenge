using AffiliateService.Api.Validators;
using FluentValidation.TestHelper;

namespace AffiliateService.Api.Tests.Unit
{
    public class ModelValidatorTests
    {
        public class InsertUpdateAffiliateValidatorTests
        {
            public InsertUpdateAffiliateValidatorTests()
            {
            }

            [Theory]
            [InlineData("Affiliate 1")]
            [InlineData("Aff")]
            [InlineData("Af f")]
            public void WithValidNames_Succeeds(string name)
            {
                // Arrange
                var validator = new InsertUpdateAffiliateValidator();
                var model = new Models.InsertUpdateAffiliate(name);

                //Act
                var result = validator.TestValidate(model);

                //Assert
                result.ShouldNotHaveValidationErrorFor(m => m.Name);
            }

            [Theory]
            [InlineData("")]
            [InlineData("A")]
            [InlineData("Af")]
            public void WithInvalidNames_Fails(string name)
            {
                // Arrange
                var validator = new InsertUpdateAffiliateValidator();
                var model = new Models.InsertUpdateAffiliate(name);

                //Act
                var result = validator.TestValidate(model);

                //Assert
                result.ShouldHaveValidationErrorFor(m => m.Name);
            }

            [Fact]
            public void WithNameLongerThan50Chars_Fails()
            {
                // Arrange
                var validator = new InsertUpdateAffiliateValidator();
                var model = new Models.InsertUpdateAffiliate("a".PadLeft(51));

                //Act
                var result = validator.TestValidate(model);

                //Assert
                result.ShouldHaveValidationErrorFor(m => m.Name);
            }
        }

    }
}