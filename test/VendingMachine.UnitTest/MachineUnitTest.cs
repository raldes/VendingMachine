using Moq;
using System.Security.Cryptography;
using System.Xml.Serialization;
using VendingMachine.App.Contracts;
using VendingMachine.App.Models;

namespace VendingMachine.UnitTest
{
    public class MachineUnitTest
    {
        public readonly Mock<IWallet> _walletMock = new();
        public readonly Mock<IProductsGetRepository> _productsRepositoryMock = new();
        public readonly Mock<ICoinsAmountGetRepository> _coinsAmountRepositoryMock = new();
        public readonly Mock<ICoinsGetRepository> _coinsRepositoryMock = new();

        [Fact]
        public void AddProduct_Success()
        {
            var product = new Product(99, "Water", 1.20M, 1);

            _productsRepositoryMock.Setup(p => p.Products());
            _walletMock.Setup(x => x.AddCoinAmount(new CoinAmount(new Coin("20c", 0.20M), 6)));
            _walletMock.Setup(x => x.GetExtraDepositedValue(product.Price)).Returns(0);

            // Arrange
            Machine machine = new(
                _productsRepositoryMock.Object,
                _walletMock.Object
                );

            // Act
            var devolution = machine.SellProduct(product);

            // Assert
            Assert.True(machine.Products.FirstOrDefault(p => p.Code == "Water") != null);
            Assert.(devolution);
        }

        //[Fact]
        public void Test1()
        {
            ////test Case 1: after sold a product the Balance should Be the  the old Balance + the product price
            //// Arrange
            //var userManagement = new UserManagement();

            //// Act
            //userManagement.Add(new(
            //        "Mohamad", "Lawand"
            //));

            //// Assert
            //var savedUser = Assert.Single(userManagement.AllUsers);
            //Assert.NotNull(savedUser);
            //Assert.Equal("Mohamad", savedUser.FirstName);
            //Assert.Equal("Lawand", savedUser.LastName);
            //Assert.NotEmpty(savedUser.Phone);
            //Assert.False(savedUser.VerifiedEmail);
        }
    }
}