using AffiliateService.Infrastructure.Repository;
using Microsoft.Extensions.Logging;
using Moq;

namespace AffiliateService.Application.Tests.Unit
{
    public class AffiliateServiceTests
    {
        /// <summary>
        /// Test InsertAsync method
        /// </summary>
        public class InsertAsync
        {
            private readonly Mock<ILogger<Services.AffiliateService>> _loggerMock;
            private readonly Mock<IAffiliateRepository> _repositoryMock;

            public InsertAsync()
            {
                _loggerMock = new Mock<ILogger<Services.AffiliateService>>();
                _repositoryMock = new Mock<IAffiliateRepository>();
            }

            [Fact]
            public async Task WithValidEntity_Succeeds()
            {
                // Arrange
                var repoAffiliate = new Domain.Entities.Affiliate
                {
                    Name = "Affiliate 1"
                };
                _repositoryMock
                    .Setup(r => r.InsertAsync(It.IsAny<Domain.Entities.Affiliate>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(repoAffiliate);

                var service = new Services.AffiliateService(_repositoryMock.Object, _loggerMock.Object);

                // Act
                var affiliate = new Domain.Entities.Affiliate
                {
                    Name = "Affiliate 1"
                };
                var result = await service.InsertAsync(affiliate);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(repoAffiliate.UniqueId, result.UniqueId);
                Assert.Equal(repoAffiliate.Name, result.Name);
            }

            [Fact]
            public async Task DateCreatedAndUniqueIdShouldBeUnique_Succeeds()
            {
                // Arrange
                var dateCreated = DateTime.Now.AddDays(1);
                var uniqueId = Guid.NewGuid();
                var affiliate = new Domain.Entities.Affiliate
                {
                    DateCreated = dateCreated,
                    UniqueId = uniqueId,
                    Name = "Affiliate 1"
                };
                _repositoryMock
                    .Setup(r => r.InsertAsync(It.IsAny<Domain.Entities.Affiliate>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(affiliate);

                var service = new Services.AffiliateService(_repositoryMock.Object, _loggerMock.Object);

                // Act
                var result = await service.InsertAsync(affiliate);

                // Assert
                Assert.NotNull(result);
                Assert.NotEqual(result.UniqueId, uniqueId);
                Assert.NotEqual(result.DateCreated, dateCreated);
            }
        }
    }
}