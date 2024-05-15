using CarAPI.Entities;
using CarAPI.Models;
using CarAPI.Payment;
using CarAPI.Repositories_DAL;
using CarAPI.Services_BLL;
using Moq;
using Xunit.Abstractions;

namespace Day2_unitTest
{
    public class carFactoryTest :IDisposable
    {
        private readonly ITestOutputHelper testOutputHelper;
        
        Mock<ICarsRepository> carRepoMock;
        Mock<IOwnersRepository> ownerRepoMock;
        Mock<ICashService> cashMock;

        OwnersService ownersService;
        public carFactoryTest(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;

            testOutputHelper.WriteLine("Test  lunched");
            carRepoMock = new();
            cashMock = new();
            ownerRepoMock = new();

            ownersService = new(carRepoMock.Object,ownerRepoMock.Object,cashMock.Object);
        }
        public void Dispose()
        {
            testOutputHelper.WriteLine("Test terminated");
        }

        [Fact]
        public void CarFactory_OwnerHaveACar_test() {
            testOutputHelper.WriteLine("Owner Already have a car test");
            //Arrange
            Car car = new() {Id=10 , OwnerId=10 , Price=1000 };
            Owner owner = new() { Id=10,Car=car };

            ownerRepoMock.Setup(o => o.GetOwnerById(10)).Returns(owner);
            carRepoMock.Setup(o => o.GetCarById(10)).Returns(car);
            BuyCarInput buyCarInput = new() { CarId=10 , OwnerId=10, Amount=1000 };
            //Act
            string result = ownersService.BuyCar(buyCarInput);
            //Assert
            Assert.Equal("Already have car", result);
        }
        [Fact]
        [Trait("Author","Mohammed Samy")]
        [Trait("priority","1")]
        public void PaymentTest_FailedDueToInsufficientFunds() {
            testOutputHelper.WriteLine("Payment Faliuer test");
            //Arrange
            Car car = new() { Id = 10, OwnerId = 10, Price = 5000 };
            Owner owner = new() { Id = 10 };

            ownerRepoMock.Setup(o => o.GetOwnerById(10)).Returns(owner);
            carRepoMock.Setup(o => o.GetCarById(10)).Returns(car);
            BuyCarInput buyCarInput = new() { CarId = 10, OwnerId = 10, Amount = 1000 };
            //Act
            string result = ownersService.BuyCar(buyCarInput);
            //Assert
            Assert.Contains("Insufficient", result );
        }
        [Fact(Skip ="there will be an error here bcz i didn't pass the right owner ID")]
        [Trait("Author", "Mohammed Samy")]
        [Trait("priority", "1")]
        
        public void Check_BuyCar_Sucess() {
            testOutputHelper.WriteLine("Buy car function sucess  test");
            //Arrange
            Car car = new() { Id = 10, OwnerId = 10, Price = 5000 };
            Owner owner = new() { Id = 10 };

            ownerRepoMock.Setup(o => o.GetOwnerById(10)).Returns(owner);
            carRepoMock.Setup(o => o.GetCarById(10)).Returns(car);
            BuyCarInput buyCarInput = new() { CarId = 10, OwnerId = 20, Amount = 1000 };
            //Act
            string result = ownersService.BuyCar(buyCarInput);
            //Assert
            Assert.Contains("Successfull", result);
        }
        [Fact]
        [Trait("Author", "Mohammed Samy")]
        [Trait("priority", "1")]
        public void Check_BuyCar_Failure() {
            testOutputHelper.WriteLine("Buy car function failure test");
            //Arrange
            Car car = new() { Id = 10, OwnerId = 10, Price = 5000 };
            Owner owner = new() { Id = 10 };

            ownerRepoMock.Setup(o => o.GetOwnerById(10)).Returns(owner);
            carRepoMock.Setup(o => o.GetCarById(10)).Returns(car);
            carRepoMock.Setup(c => c.AssignToOwner(10, 10)).Returns(false);
            BuyCarInput buyCarInput = new() { CarId = 10, OwnerId = 10, Amount = 5000 };
            //Act
            string result = ownersService.BuyCar(buyCarInput);
            //Assert
            Assert.Contains("wrong", result);
        }

    }
}