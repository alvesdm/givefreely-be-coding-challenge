using AffiliateService.Infrastructure.Persistence;
using AffiliateService.Infrastructure.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;

namespace AffiliateService.Infrastructure.Tests.Unit
{
    public class CustomerRepositoryTests
    {
        public class GetAsync
        {
            private readonly Mock<ILogger<CustomerRepository>> _loggerMock;
            private readonly Mock<AffiliateDbContext> _dbContextMock;

            public GetAsync()
            {
                _loggerMock = new Mock<ILogger<CustomerRepository>>();
                _dbContextMock = new Mock<AffiliateDbContext>();

                //default setup, that can be overiden if required
                _dbContextMock.Setup(x => x.Customers)
                    .ReturnsDbSet(TestDataHelper.Customers.GetFakeList());
            }

            [Fact]
            public async Task WithExistingEntity_Succeeds()
            {
                // Arrange
                var _repository = new CustomerRepository(_dbContextMock.Object, _loggerMock.Object);
                var customer = TestDataHelper.Customers.GetFakeList().First();
                //Act
                var ex = Record.ExceptionAsync(async() => await _repository.GetAsync(customer.UniqueId));

                //Assert
                ex.Result.Should().BeNull();
            }

            [Fact]
            public async Task WithNonExistingEntity_ShouldNotThrowException()
            {
                // Arrange
                var _repository = new CustomerRepository(_dbContextMock.Object, _loggerMock.Object);
                var customer = TestDataHelper.Customers.GetFakeList().First();
                //Act
                var ex = Record.ExceptionAsync(async () => await _repository.GetAsync(1000));

                //Assert
                ex.Result.Should().BeNull();
            }

            [Fact]
            public async Task WithNonExistingEntity_ReturnsNull()
            {
                // Arrange
                var _repository = new CustomerRepository(_dbContextMock.Object, _loggerMock.Object);
                var customer = TestDataHelper.Customers.GetFakeList().First();
                //Act
                var result = await _repository.GetAsync(1000);

                //Assert
                result.Should().BeNull();
            }
        }
    }
}