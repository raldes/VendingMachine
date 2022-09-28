using VendingMachine.App.Contracts;
using VendingMachine.App.Models;

namespace VendingMachine.Data
{
    public class ProductsGetRepository : IProductsGetRepository
    {
        private readonly ICollection<Product> _products = new List<Product>();

        public ProductsGetRepository()
        {
            LoadProducts();
        }

        public void LoadProducts()
        {
            int key = 0;

            var tea = new Product(key: key++, code: "Tea", price: 1.3M, portions: 10);
            _products.Add(tea);

            var espresso = new Product(key: key++, code: "Espresso", price: 1.8M, portions: 20);
            _products.Add(espresso);

            var juice = new Product(key: key++, code: "Juice", price: 1.83M, portions: 20);
            _products.Add(juice);

            var chicken_soup = new Product(key: key++, code: "Chicken soup", price: 0.1M, portions: 15);
            _products.Add(chicken_soup);
        }

        public ICollection<Product> Get()
        {
            return _products;
        }
    }
}
