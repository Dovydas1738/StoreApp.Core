using StoreApp.Core.Models;
using StoreApp.Core.Contracts;
using StoreApp.Core.Repositories;
using StoreApp.Core.Services;
using StoreApp.Core.Enums;
using Moq;
using System;

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

        [Fact]
        public async Task Add_Buyer_Success_Test()
        {
            // Arrange
            Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
            Mock<IMongoCacheRepository> _mongoRepository = new Mock<IMongoCacheRepository>();
            IUserService userService = new UserService(_userRepository.Object, _mongoRepository.Object);

            Buyer buyer = new Buyer
            {
                BuyerId = 1,
                Name = "Test",
                Surname = "TestSurname",
                Email = "TestEmail",
                PhoneNumber = "1234567890",
                IsInLoyaltyProgram = true,
            };

            Buyer buyer2 = new Buyer
            {
                BuyerId = 2,
                Name = "Test2",
                Surname = "TestSurname2",
                Email = "TestEmail",
                PhoneNumber = "1234567890",
                IsInLoyaltyProgram = false,
            };

            var buyerList = new List<Buyer> { buyer };
            _userRepository.Setup(x => x.GetAllBuyers()).ReturnsAsync(buyerList);
            _mongoRepository.Setup(x => x.GetAllBuyers()).ReturnsAsync(buyerList);

            // Act
            await userService.AddBuyer(buyer2);

            // Assert
            _userRepository.Verify(x => x.AddBuyer(It.IsAny<Buyer>()), Times.Once);
            _mongoRepository.Verify(x => x.AddBuyer(It.IsAny<Buyer>()), Times.Once);
        }

        [Fact]
        public async Task Add_Buyer_Failure_Test()
        {
            // Arrange
            Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
            Mock<IMongoCacheRepository> _mongoRepository = new Mock<IMongoCacheRepository>();
            IUserService userService = new UserService(_userRepository.Object, _mongoRepository.Object);

            Buyer buyer = new Buyer
            {
                BuyerId = 1,
                Name = "Test",
                Surname = "TestSurname",
                Email = "TestEmail",
                PhoneNumber = "1234567890",
                IsInLoyaltyProgram = true,
            };

            Buyer buyer2 = new Buyer
            {
                BuyerId = 2,
                Name = "Test2",
                Surname = "TestSurname2",
                Email = "TestEmail",
                PhoneNumber = "1234567890",
                IsInLoyaltyProgram = false,
            };

            var buyerList = new List<Buyer> { buyer };
            _userRepository.Setup(x => x.GetAllBuyers()).ReturnsAsync(buyerList);
            _mongoRepository.Setup(x => x.GetAllBuyers()).ReturnsAsync(buyerList);

            // Act
            await userService.AddBuyer(buyer2);

            // Assert
            _userRepository.Verify(x => x.AddBuyer(It.IsAny<Buyer>()), Times.Once);
            _mongoRepository.Verify(x => x.AddBuyer(It.IsAny<Buyer>()), Times.Once);

            await Assert.ThrowsAsync<Exception>(() => userService.AddBuyer(buyer));
        }

        [Fact]
        public async Task Add_Seller_Success_Test()
        {
            // Arrange
            Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
            Mock<IMongoCacheRepository> _mongoRepository = new Mock<IMongoCacheRepository>();
            IUserService userService = new UserService(_userRepository.Object, _mongoRepository.Object);

            Seller seller = new Seller
            {
                SellerId = 1,
                Name = "Test",
                Surname = "TestSurname",
                Email = "TestEmail",
                PhoneNumber = "1234567890",
                Position = SellerPosition.Consultant,
            };

            Seller seller2 = new Seller
            {
                SellerId = 2,
                Name = "Test2",
                Surname = "TestSurname2",
                Email = "TestEmail",
                PhoneNumber = "1234567890",
                Position = SellerPosition.Cashier,
            };

            var sellerList = new List<Seller> { seller };
            _userRepository.Setup(x => x.GetAllSellers()).ReturnsAsync(sellerList);
            _mongoRepository.Setup(x => x.GetAllSellers()).ReturnsAsync(sellerList);

            // Act
            await userService.AddSeller(seller2);

            // Assert
            _userRepository.Verify(x => x.AddSeller(It.IsAny<Seller>()), Times.Once);
            _mongoRepository.Verify(x => x.AddSeller(It.IsAny<Seller>()), Times.Once);
        }

        [Fact]
        public async Task Add_Seller_Failure_Test()
        {
            // Arrange
            Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
            Mock<IMongoCacheRepository> _mongoRepository = new Mock<IMongoCacheRepository>();
            IUserService userService = new UserService(_userRepository.Object, _mongoRepository.Object);

            Seller seller = new Seller
            {
                SellerId = 1,
                Name = "Test",
                Surname = "TestSurname",
                Email = "TestEmail",
                PhoneNumber = "1234567890",
                Position = SellerPosition.Consultant,
            };

            Seller seller2 = new Seller
            {
                SellerId = 2,
                Name = "Test2",
                Surname = "TestSurname2",
                Email = "TestEmail",
                PhoneNumber = "1234567890",
                Position = SellerPosition.Cashier,
            };

            var sellerList = new List<Seller> { seller };
            _userRepository.Setup(x => x.GetAllSellers()).ReturnsAsync(sellerList);
            _mongoRepository.Setup(x => x.GetAllSellers()).ReturnsAsync(sellerList);

            // Act
            await userService.AddSeller(seller2);

            // Assert
            _userRepository.Verify(x => x.AddSeller(It.IsAny<Seller>()), Times.Once);
            _mongoRepository.Verify(x => x.AddSeller(It.IsAny<Seller>()), Times.Once);

            await Assert.ThrowsAsync<Exception>(() => userService.AddSeller(seller));
        }


    }
}