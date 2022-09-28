using Moq;
using VendingMachine.App.Contracts;
using VendingMachine.App.Exceptions;
using VendingMachine.App.Models;

namespace MachineUnitTest
{
    public class MachineTest
    {
        public readonly Mock<IWallet> _walletMock = new();
        public readonly Mock<IProductsGetRepository> _productsGetRepositoryMock = new();

        [Fact]
        public void AddProduct_Success_WithChange()
        {
            var product = new Product(90, "Chocolate", 1.50M, 1);

            var change = new List<CoinAmount>();
            change.Add(new CoinAmount(new Coin(code: "1e", 1.0M), 1));

            _walletMock.Setup(x => x.OnSoldProduct(It.IsAny<Product>())).Returns(change);

            Machine machine = new(_productsGetRepositoryMock.Object, _walletMock.Object);
            var result = machine.SellProduct(product);

            Assert.NotNull(result);
            Assert.True(result.Any());
        }

        [Fact]
        public void AddProduct_Success_WithoutChange()
        {
            var product = new Product(90, "Chocolate", 1.50M, 1);

            _walletMock.Setup(x => x.OnSoldProduct(It.IsAny<Product>())).Returns(new List<CoinAmount>());

            Machine machine = new(_productsGetRepositoryMock.Object, _walletMock.Object);
            var result = machine.SellProduct(product);

            Assert.NotNull(result);
        }

        [Fact]
        public void AddProduct_Fail_InsuficientBalance()
        {
            var product = new Product(90, "Chocolate", 1.50M, 1);

            _walletMock.Setup(x => x.OnSoldProduct(It.IsAny<Product>())).Throws<InsuficientBalanceException>();

            Machine machine = new(_productsGetRepositoryMock.Object, _walletMock.Object);

            Assert.Throws<InsuficientBalanceException>(() => machine.SellProduct(product));
        }
        
        [Fact]
        public void AddProduct_Fail_HaveNoCoinsForChange()
        {
            var product = new Product(90, "Chocolate", 1.50M, 1);

            _walletMock.Setup(x => x.OnSoldProduct(It.IsAny<Product>())).Throws<ChangeHaveNoSolutionException>();

            Machine machine = new(_productsGetRepositoryMock.Object, _walletMock.Object);

            Assert.Throws<ChangeHaveNoSolutionException>(() => machine.SellProduct(product));
        }
    }
}