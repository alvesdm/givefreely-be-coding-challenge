using AffiliateService.Infrastructure.Persistence;
using AffiliateService.Infrastructure.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;

namespace AffiliateService.Infrastructure.Tests.Unit
{
    public class AffiliateRepositoryTests
    {
        public class RemoveAsync
        {
            private readonly Mock<ILogger<AffiliateRepository>> _loggerMock;
            private readonly Mock<AffiliateDbContext> _dbContextMock;

            public RemoveAsync()
            {
                _loggerMock = new Mock<ILogger<AffiliateRepository>>();
                _dbContextMock = new Mock<AffiliateDbContext>();

                //default setup, that can be overiden if required
                _dbContextMock.Setup(x => x.Affiliates)
                    .ReturnsDbSet(TestDataHelper.Affiliates.GetFakeList());
            }

            [Fact]
            public async Task WithExistingEntity_Succeedd()
            {
                // Arrange
                var _repository = new AffiliateRepository(_dbContextMock.Object, _loggerMock.Object);
                var affiliate = TestDataHelper.Affiliates.GetFakeList().First();
                //Act
                var ex = Record.ExceptionAsync(async() => await _repository.RemoveAsync(affiliate.UniqueId));

                //Assert
                ex.Result.Should().BeNull();
            }

            [Fact]
            public async Task WithNonExistingEntity_Fails()
            {
                // Arrange
                var _repository = new AffiliateRepository(_dbContextMock.Object, _loggerMock.Object);
                var affiliate = TestDataHelper.Affiliates.GetFakeList().First();
                //Act
                var ex = Record.ExceptionAsync(async () => await _repository.RemoveAsync(Guid.NewGuid()));

                //Assert
                ex.Result.Should().NotBeNull();
                ex.Result.Should().BeOfType<NotFoundHttpException>();
            }
        }
    }
}