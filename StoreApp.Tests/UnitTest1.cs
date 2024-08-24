using StoreApp.Core.Models;
using StoreApp.Core.Contracts;
using StoreApp.Core.Repositories;
using StoreApp.Core.Services;
using StoreApp.Core.Enums;
using Moq;

namespace StoreApp.Tests
{
    public class UnitTests
    {
        // Product tests

        [Fact]
        public async Task Add_Product_Success_Test()
        {
            // Arrange
            Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();
            IProductService productService = new ProductService( _productRepository.Object );

            Product product = new Product
            {
                ProductId = 1,
                ProductName = "Test",
                Price = 10m,
                Category = ProductCategory.Food,
                AmountInStorage = 3,
            };

            Product product2 = new Product
            {
                ProductId = 2,
                ProductName = "Test2",
                Price = 10m,
                Category = ProductCategory.Food,
                AmountInStorage = 3,
            };

            var productList = new List<Product> { product2 };
            _productRepository.Setup(x => x.GetAllProducts()).ReturnsAsync(productList);

            // Act
            await productService.AddProduct(product);

            // Assert
            Assert.NotNull(await productService.GetAllProducts());
            _productRepository.Verify(x => x.AddProduct(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task Add_Product_Duplicate_Test()
        {
            // Arrange
            Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();
            IProductService productService = new ProductService(_productRepository.Object);

            Product product = new Product
            {
                ProductId = 1,
                ProductName = "Test",
                Price = 10m,
                Category = ProductCategory.Food,
                AmountInStorage = 3,
            };

            Product product2 = new Product
            {
                ProductId = 2,
                ProductName = "Test2",
                Price = 10m,
                Category = ProductCategory.Food,
                AmountInStorage = 3,
            };

            var productList = new List<Product> { product, product2 };
            _productRepository.Setup(x => x.GetAllProducts()).ReturnsAsync(productList);
            int newAmount = 6;

            // Act
            await productService.AddProduct(product);

            // Arange
            Assert.Equal(6, product.AmountInStorage);
            Assert.Equal(3, product2.AmountInStorage);
        }

        [Fact]
        public async Task Get_All_Products_Test()
        {
            // Arrange
            Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();
            IProductService productService = new ProductService(_productRepository.Object);

            Product product = new Product
            {
                ProductId = 1,
                ProductName = "Test",
                Price = 10m,
                Category = ProductCategory.Food,
                AmountInStorage = 3,
            };

            Product product2 = new Product
            {
                ProductId = 2,
                ProductName = "Test2",
                Price = 10m,
                Category = ProductCategory.Food,
                AmountInStorage = 3,
            };

            var productList = new List<Product> { product, product2 };
            _productRepository.Setup(x => x.GetAllProducts()).ReturnsAsync(productList);

            // Act
            List<Product> allProducts = await productService.GetAllProducts();

            // Assert
            Assert.NotNull(allProducts);
            Assert.Equal(2, allProducts.Count);
        }

        [Fact]
        public async Task Get_Product_By_Id_Success_Test()
        {
            // Arrange
            Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();
            IProductService productService = new ProductService(_productRepository.Object);

            Product product = new Product
            {
                ProductId = 1,
                ProductName = "Test",
                Price = 10m,
                Category = ProductCategory.Food,
                AmountInStorage = 3,
            };

            Product product2 = new Product
            {
                ProductId = 2,
                ProductName = "Test2",
                Price = 10m,
                Category = ProductCategory.Food,
                AmountInStorage = 3,
            };

            _productRepository.Setup(x => x.GetProductById(1)).ReturnsAsync(product);
            _productRepository.Setup(x => x.GetProductById(2)).ReturnsAsync(product2);

            // Act

            Product foundProduct = await productService.GetProductById(1);
            Product foundProduct2 = await productService.GetProductById(2);

            // Assert

            Assert.NotNull(foundProduct);
            Assert.NotNull(foundProduct2);
            Assert.Equal(product, foundProduct);
            Assert.Equal(product2, foundProduct2);
        }

        [Fact]
        public async Task Get_Product_By_Id_Failure_Test()
        {
            // Arrange
            Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();
            IProductService productService = new ProductService(_productRepository.Object);

            Product product = new Product
            {
                ProductId = 1,
                ProductName = "Test",
                Price = 10m,
                Category = ProductCategory.Food,
                AmountInStorage = 3,
            };

            Product product2 = new Product
            {
                ProductId = 2,
                ProductName = "Test2",
                Price = 10m,
                Category = ProductCategory.Food,
                AmountInStorage = 3,
            };

            _productRepository.Setup(x => x.GetProductById(1)).ReturnsAsync(product);
            _productRepository.Setup(x => x.GetProductById(2)).ReturnsAsync(product2);

            // Act

            Product foundProduct = await productService.GetProductById(1);
            Product foundProduct2 = await productService.GetProductById(2);

            // Assert

            Assert.NotNull(foundProduct);
            Assert.NotNull(foundProduct2);
            Assert.Equal(product, foundProduct);
            Assert.Equal(product2, foundProduct2);
            await Assert.ThrowsAsync<Exception>(() => productService.GetProductById(3));
        }

        [Fact]
        public async Task Remove_Product_Success_Test()
        {
            // Arrange
            Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();
            IProductService productService = new ProductService(_productRepository.Object);

            Product product = new Product
            {
                ProductId = 1,
                ProductName = "Test",
                Price = 10m,
                Category = ProductCategory.Food,
                AmountInStorage = 3,
            };

            Product product2 = new Product
            {
                ProductId = 2,
                ProductName = "Test2",
                Price = 10m,
                Category = ProductCategory.Food,
                AmountInStorage = 3,
            };

            var productList = new List<Product> { product, product2 };
            _productRepository.Setup(x => x.GetAllProducts()).ReturnsAsync(productList);
            _productRepository.Setup(x => x.GetProductById(1)).ReturnsAsync(product);
            _productRepository.Setup(x => x.GetProductById(2)).ReturnsAsync(product2);
            _productRepository.Setup(x => x.RemoveProductById(It.IsAny<int>())).Callback<int>(id => productList.RemoveAll(p => p.ProductId == id));

            // Act
            await productService.RemoveProductById(1);


            // Assert
            Assert.NotNull(await productService.GetAllProducts());
            Assert.Equal(1, productService.GetAllProducts().Result.Count);
            Assert.Single(productService.GetAllProducts().Result);
        }

        [Fact]
        public async Task Remove_Product_Failure_NotFound_Test()
        {
            // Arrange
            Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();
            IProductService productService = new ProductService(_productRepository.Object);

            Product product = new Product
            {
                ProductId = 1,
                ProductName = "Test",
                Price = 10m,
                Category = ProductCategory.Food,
                AmountInStorage = 3,
            };

            Product product2 = new Product
            {
                ProductId = 2,
                ProductName = "Test2",
                Price = 10m,
                Category = ProductCategory.Food,
                AmountInStorage = 3,
            };

            var productList = new List<Product> { product, product2 };
            _productRepository.Setup(x => x.GetAllProducts()).ReturnsAsync(productList);
            _productRepository.Setup(x => x.GetProductById(1)).ReturnsAsync(product);
            _productRepository.Setup(x => x.GetProductById(2)).ReturnsAsync(product2);
            _productRepository.Setup(x => x.RemoveProductById(It.IsAny<int>())).Callback<int>(id => productList.RemoveAll(p => p.ProductId == id));

            // Act

            // Assert
            Assert.NotNull(await productService.GetAllProducts());
            Assert.Equal(2, productService.GetAllProducts().Result.Count);
            await Assert.ThrowsAsync<Exception>(()=> productService.RemoveProductById(3));
        }

        [Fact]
        public async Task Update_Product_Success_Test()
        {
            // Arrange
            Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();
            IProductService productService = new ProductService(_productRepository.Object);

            Product product = new Product
            {
                ProductId = 1,
                ProductName = "Test",
                Price = 10m,
                Category = ProductCategory.Food,
                AmountInStorage = 3,
            };

            Product product2 = new Product
            {
                ProductId = 2,
                ProductName = "Test2",
                Price = 10m,
                Category = ProductCategory.Food,
                AmountInStorage = 3,
            };

            Product productUpdate = new Product
            {
                ProductId = 1,
                ProductName = "Test",
                Price = 20m,
                Category = ProductCategory.Hardware,
                AmountInStorage = 5,
            };


            var productList = new List<Product> { product, product2 };
            _productRepository.Setup(x => x.GetAllProducts()).ReturnsAsync(productList);
            _productRepository.Setup(x => x.GetProductById(1)).ReturnsAsync(product);
            _productRepository.Setup(x => x.UpdateProduct(It.IsAny<Product>()))
                .Callback<Product>(p =>
                {
                    var productToUpdate = productList.FirstOrDefault(x => x.ProductId == p.ProductId);
                    if (productToUpdate != null)
                    {
                        productToUpdate.ProductName = p.ProductName;
                        productToUpdate.Price = p.Price;
                        productToUpdate.Category = p.Category;
                        productToUpdate.AmountInStorage = p.AmountInStorage;
                    }
                });

            // Act

            await productService.UpdateProduct(productUpdate);

            // Assert
            Assert.NotNull(await productService.GetAllProducts());
            Assert.Equal(2, productService.GetAllProducts().Result.Count);
            _productRepository.Verify(x => x.UpdateProduct(It.IsAny<Product>()), Times.Once);

            Assert.Equal(productList[0].Price, productUpdate.Price);
            Assert.Equal(productList[0].AmountInStorage, productUpdate.AmountInStorage);
            Assert.NotEqual(productList[1].Price, productUpdate.Price);
            Assert.NotEqual(productList[1].AmountInStorage, productUpdate.AmountInStorage);
        }

        [Fact]
        public async Task Update_Product_Failure_NotFound_Test()
        {
            // Arrange
            Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();
            IProductService productService = new ProductService(_productRepository.Object);

            Product product = new Product
            {
                ProductId = 1,
                ProductName = "Test",
                Price = 10m,
                Category = ProductCategory.Food,
                AmountInStorage = 3,
            };

            Product product2 = new Product
            {
                ProductId = 2,
                ProductName = "Test2",
                Price = 10m,
                Category = ProductCategory.Food,
                AmountInStorage = 3,
            };

            Product productUpdate = new Product
            {
                ProductId = 1,
                ProductName = "Test",
                Price = 20m,
                Category = ProductCategory.Hardware,
                AmountInStorage = 5,
            };


            var productList = new List<Product> { product, product2 };
            _productRepository.Setup(x => x.GetAllProducts()).ReturnsAsync(productList);
            _productRepository.Setup(x => x.GetProductById(3)).ReturnsAsync(product);
            _productRepository.Setup(x => x.UpdateProduct(It.IsAny<Product>()))
                .Callback<Product>(p =>
                {
                    var productToUpdate = productList.FirstOrDefault(x => x.ProductId == p.ProductId);
                    if (productToUpdate != null)
                    {
                        productToUpdate.ProductName = p.ProductName;
                        productToUpdate.Price = p.Price;
                        productToUpdate.Category = p.Category;
                        productToUpdate.AmountInStorage = p.AmountInStorage;
                    }
                });

            // Act

            // Assert
            Assert.NotNull(await productService.GetAllProducts());
            Assert.Equal(2, productService.GetAllProducts().Result.Count);

            await Assert.ThrowsAsync<Exception>(() => productService.UpdateProduct(productUpdate));
        }

        // User tests


    }
}