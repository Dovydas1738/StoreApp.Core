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
            Assert.Single(await productService.GetAllProducts());
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

        [Fact]
        public async Task Get_All_Buyers_Test()
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

            var buyerListCache = new List<Buyer> { buyer };
            var buyerListDb = new List<Buyer> { buyer, buyer2 };

            _userRepository.Setup(x => x.GetAllBuyers()).ReturnsAsync(buyerListDb);
            _mongoRepository.SetupSequence(x => x.GetAllBuyers())
                .ReturnsAsync(buyerListCache)
                .ReturnsAsync(buyerListDb);

            // Act
            List<Buyer> allBuyers = await userService.GetAllBuyers();

            // Assert
            Assert.NotNull(allBuyers);
            Assert.Equal(2, allBuyers.Count);

            _mongoRepository.Verify(x => x.ClearBuyersCache(), Times.Once);
            _mongoRepository.Verify(x => x.AddBuyer(It.IsAny<Buyer>()), Times.Exactly(buyerListDb.Count));
            _mongoRepository.Verify(x => x.GetAllBuyers(), Times.Exactly(2));
        }

        [Fact]
        public async Task Get_All_Sellers_Test()
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

            var sellerListCache = new List<Seller> { seller };
            var sellerListDb = new List<Seller> { seller, seller2 };

            _userRepository.Setup(x => x.GetAllSellers()).ReturnsAsync(sellerListDb);
            _mongoRepository.SetupSequence(x => x.GetAllSellers())
                .ReturnsAsync(sellerListCache)   
                .ReturnsAsync(sellerListDb);

            // Act
            List<Seller>allSellers = await userService.GetAllSellers();

            // Assert
            Assert.NotNull(allSellers);
            Assert.Equal(2, allSellers.Count);

            _mongoRepository.Verify(x => x.ClearSellersCache(), Times.Once);
            _mongoRepository.Verify(x => x.AddSeller(It.IsAny<Seller>()), Times.Exactly(sellerListDb.Count));
            _mongoRepository.Verify(x => x.GetAllSellers(), Times.Exactly(2));
        }

        [Fact]
        public async Task Remove_Buyer_By_Id_Success_Test()
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

            var buyerListCache = new List<Buyer> { buyer };
            var buyerListDb = new List<Buyer> { buyer, buyer2 };

            _userRepository.Setup(x => x.GetAllBuyers()).ReturnsAsync(buyerListDb);
            _userRepository.Setup(x => x.GetBuyerById(1)).ReturnsAsync(buyer);
            _userRepository.Setup(x => x.GetBuyerById(2)).ReturnsAsync(buyer2);
            _userRepository.Setup(x => x.RemoveBuyerById(It.IsAny<int>())).Callback<int>(id => buyerListDb.RemoveAll(b => b.BuyerId == id));

            _mongoRepository.Setup(x => x.GetAllBuyers()).ReturnsAsync(buyerListCache);
            _mongoRepository.Setup(x => x.GetBuyerById(1)).ReturnsAsync(buyer);
            _mongoRepository.Setup(x => x.GetBuyerById(2)).ReturnsAsync(buyer2);
            _mongoRepository.Setup(x => x.RemoveBuyerById(It.IsAny<int>())).Callback<int>(id => buyerListCache.RemoveAll(b => b.BuyerId == id));

            // Act
            await userService.RemoveBuyerById(1);

            // Assert
            Assert.NotNull(buyerListDb);
            Assert.Empty(buyerListCache);
            Assert.Single(buyerListDb);

        }

        [Fact]
        public async Task Remove_Buyer_By_Id_Failure_NotFound_Test()
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

            var buyerListCache = new List<Buyer> { buyer };
            var buyerListDb = new List<Buyer> { buyer, buyer2 };

            _userRepository.Setup(x => x.GetAllBuyers()).ReturnsAsync(buyerListDb);
            _userRepository.Setup(x => x.GetBuyerById(1)).ReturnsAsync(buyer);
            _userRepository.Setup(x => x.GetBuyerById(2)).ReturnsAsync(buyer2);
            _userRepository.Setup(x => x.RemoveBuyerById(It.IsAny<int>())).Callback<int>(id => buyerListDb.RemoveAll(b => b.BuyerId == id));

            _mongoRepository.Setup(x => x.GetAllBuyers()).ReturnsAsync(buyerListCache);
            _mongoRepository.Setup(x => x.GetBuyerById(1)).ReturnsAsync(buyer);
            _mongoRepository.Setup(x => x.GetBuyerById(2)).ReturnsAsync(buyer2);
            _mongoRepository.Setup(x => x.RemoveBuyerById(It.IsAny<int>())).Callback<int>(id => buyerListCache.RemoveAll(b => b.BuyerId == id));

            // Act
            await userService.RemoveBuyerById(1);

            // Assert
            Assert.NotNull(buyerListDb);
            Assert.Empty(buyerListCache);
            Assert.Single(buyerListDb);
            await Assert.ThrowsAsync<Exception>(() => userService.RemoveBuyerById(3));
        }

        [Fact]
        public async Task Get_Buyer_By_Id_Success_Test()
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

            var buyerListCache = new List<Buyer> { buyer };
            var buyerListDb = new List<Buyer> { buyer, buyer2 };

            _userRepository.Setup(x => x.GetBuyerById(1)).ReturnsAsync(buyer);
            _userRepository.Setup(x => x.GetBuyerById(2)).ReturnsAsync(buyer2);

            _mongoRepository.Setup(x => x.GetBuyerById(It.IsAny<int>()))
                   .ReturnsAsync((int id) => buyerListCache.FirstOrDefault(b => b.BuyerId == id));

            _mongoRepository.Setup(x => x.AddBuyer(It.IsAny<Buyer>())).Callback<Buyer>(buyer => buyerListCache.Add(buyer));

            // Act
            Buyer foundBuyer = await userService.GetBuyerById(1);
            Buyer foundBuyer2 = await userService.GetBuyerById(2);

            // Assert
            Assert.NotNull(foundBuyer);
            _mongoRepository.Verify(x => x.AddBuyer(It.IsAny<Buyer>()), Times.Once);
            Assert.NotNull(foundBuyer2);
            Assert.Equal(2, buyerListCache.Count);
            Assert.Equal(2, buyerListDb.Count);

        }

        [Fact]
        public async Task Get_Buyer_By_Id_Failure_NotFound_Test()
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

            var buyerListCache = new List<Buyer> { buyer };
            var buyerListDb = new List<Buyer> { buyer, buyer2 };

            _userRepository.Setup(x => x.GetBuyerById(1)).ReturnsAsync(buyer);
            _userRepository.Setup(x => x.GetBuyerById(2)).ReturnsAsync(buyer2);

            _mongoRepository.Setup(x => x.GetBuyerById(It.IsAny<int>()))
                   .ReturnsAsync((int id) => buyerListCache.FirstOrDefault(b => b.BuyerId == id));

            _mongoRepository.Setup(x => x.AddBuyer(It.IsAny<Buyer>())).Callback<Buyer>(buyer => buyerListCache.Add(buyer));

            // Act
            Buyer foundBuyer = await userService.GetBuyerById(1);
            Buyer foundBuyer2 = await userService.GetBuyerById(2);

            // Assert
            Assert.NotNull(foundBuyer);
            _mongoRepository.Verify(x => x.AddBuyer(It.IsAny<Buyer>()), Times.Once);
            Assert.NotNull(foundBuyer2);
            Assert.Equal(2, buyerListCache.Count);
            Assert.Equal(2, buyerListDb.Count);
            await Assert.ThrowsAsync<Exception>(() => userService.GetBuyerById(3));

        }

        [Fact]
        public async Task Remove_Seller_By_Id_Success_Test()
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

            var sellerListCache = new List<Seller> { seller };
            var sellerListDb = new List<Seller> { seller, seller2 };

            _userRepository.Setup(x => x.GetAllSellers()).ReturnsAsync(sellerListDb);
            _userRepository.Setup(x => x.GetSellerById(1)).ReturnsAsync(seller);
            _userRepository.Setup(x => x.GetSellerById(2)).ReturnsAsync(seller2);
            _userRepository.Setup(x => x.RemoveSellerById(It.IsAny<int>())).Callback<int>(id => sellerListDb.RemoveAll(s => s.SellerId == id));

            _mongoRepository.Setup(x => x.GetAllSellers()).ReturnsAsync(sellerListCache);
            _mongoRepository.Setup(x => x.GetSellerById(1)).ReturnsAsync(seller);
            _mongoRepository.Setup(x => x.GetSellerById(2)).ReturnsAsync(seller2);
            _mongoRepository.Setup(x => x.RemoveSellerById(It.IsAny<int>())).Callback<int>(id => sellerListCache.RemoveAll(s => s.SellerId == id));

            // Act
            await userService.RemoveSellerById(1);

            // Assert
            Assert.NotNull(sellerListDb);
            Assert.Empty(sellerListCache);
            Assert.Single(sellerListDb);

        }

        [Fact]
        public async Task Remove_Seller_By_Id_Failure_NotFound_Test()
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

            var sellerListCache = new List<Seller> { seller };
            var sellerListDb = new List<Seller> { seller, seller2 };

            _userRepository.Setup(x => x.GetAllSellers()).ReturnsAsync(sellerListDb);
            _userRepository.Setup(x => x.GetSellerById(1)).ReturnsAsync(seller);
            _userRepository.Setup(x => x.GetSellerById(2)).ReturnsAsync(seller2);
            _userRepository.Setup(x => x.RemoveSellerById(It.IsAny<int>())).Callback<int>(id => sellerListDb.RemoveAll(s => s.SellerId == id));

            _mongoRepository.Setup(x => x.GetAllSellers()).ReturnsAsync(sellerListCache);
            _mongoRepository.Setup(x => x.GetSellerById(1)).ReturnsAsync(seller);
            _mongoRepository.Setup(x => x.GetSellerById(2)).ReturnsAsync(seller2);
            _mongoRepository.Setup(x => x.RemoveSellerById(It.IsAny<int>())).Callback<int>(id => sellerListCache.RemoveAll(s => s.SellerId == id));

            // Act
            await userService.RemoveSellerById(1);

            // Assert
            Assert.NotNull(sellerListDb);
            Assert.Empty(sellerListCache);
            Assert.Single(sellerListDb);
            await Assert.ThrowsAsync<Exception>(() => userService.RemoveSellerById(3));
        }

        [Fact]
        public async Task Get_Seller_By_Id_Success_Test()
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

            var sellerListCache = new List<Seller> { seller };
            var sellerListDb = new List<Seller> { seller, seller2 };

            _userRepository.Setup(x => x.GetSellerById(1)).ReturnsAsync(seller);
            _userRepository.Setup(x => x.GetSellerById(2)).ReturnsAsync(seller2);

            _mongoRepository.Setup(x => x.GetSellerById(It.IsAny<int>()))
                   .ReturnsAsync((int id) => sellerListCache.FirstOrDefault(s => s.SellerId == id));

            _mongoRepository.Setup(x => x.AddSeller(It.IsAny<Seller>())).Callback<Seller>(seller => sellerListCache.Add(seller));

            // Act
            Seller foundSeller = await userService.GetSellerById(1);
            Seller foundSeller2 = await userService.GetSellerById(2);

            // Assert
            Assert.NotNull(foundSeller);
            _mongoRepository.Verify(x => x.AddSeller(It.IsAny<Seller>()), Times.Once);
            Assert.NotNull(foundSeller2);
            Assert.Equal(2, sellerListCache.Count);
            Assert.Equal(2, sellerListDb.Count);

        }

        [Fact]
        public async Task Get_Seller_By_Id_Failure_NotFound_Test()
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

            var sellerListCache = new List<Seller> { seller };
            var sellerListDb = new List<Seller> { seller, seller2 };

            _userRepository.Setup(x => x.GetSellerById(1)).ReturnsAsync(seller);
            _userRepository.Setup(x => x.GetSellerById(2)).ReturnsAsync(seller2);

            _mongoRepository.Setup(x => x.GetSellerById(It.IsAny<int>()))
                   .ReturnsAsync((int id) => sellerListCache.FirstOrDefault(s => s.SellerId == id));

            _mongoRepository.Setup(x => x.AddSeller(It.IsAny<Seller>())).Callback<Seller>(seller => sellerListCache.Add(seller));

            // Act
            Seller foundSeller = await userService.GetSellerById(1);
            Seller foundSeller2 = await userService.GetSellerById(2);

            // Assert
            Assert.NotNull(foundSeller);
            _mongoRepository.Verify(x => x.AddSeller(It.IsAny<Seller>()), Times.Once);
            Assert.NotNull(foundSeller2);
            Assert.Equal(2, sellerListCache.Count);
            Assert.Equal(2, sellerListDb.Count);
            await Assert.ThrowsAsync<Exception>(() => userService.GetSellerById(3));
        }

        [Fact]
        public async Task Update_Buyer_Success_Test()
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

            Buyer buyerUpdate = new Buyer
            {
                BuyerId = 1,
                Name = "Test500",
                Surname = "TestSurname",
                Email = "TestEmail",
                PhoneNumber = "1234567890",
                IsInLoyaltyProgram = false,

            };

            var buyerListCache = new List<Buyer> { buyer };
            var buyerListDb = new List<Buyer> { buyer, buyer2 };

            _userRepository.Setup(x => x.GetAllBuyers()).ReturnsAsync(buyerListDb);
            _userRepository.Setup(x => x.GetBuyerById(1)).ReturnsAsync(buyer);
            _userRepository.Setup(x => x.UpdateBuyer(It.IsAny<Buyer>()))
                .Callback<Buyer>(b =>
                {
                    var buyerToUpdate = buyerListDb.FirstOrDefault(x => x.BuyerId == b.BuyerId);
                    if (buyerToUpdate != null)
                    {
                        buyerToUpdate.Name = b.Name;
                        buyerToUpdate.Surname = b.Surname;
                        buyerToUpdate.Email = b.Email;
                        buyerToUpdate.PhoneNumber = b.PhoneNumber;
                        buyerToUpdate.IsInLoyaltyProgram = b.IsInLoyaltyProgram;
                    }
                });

            _mongoRepository.Setup(x => x.GetAllBuyers()).ReturnsAsync(buyerListCache);
            _mongoRepository.Setup(x => x.GetBuyerById(1)).ReturnsAsync(buyer);
            _mongoRepository.Setup(x => x.UpdateBuyer(It.IsAny<Buyer>()))
                .Callback<Buyer>(b =>
                {
                    var buyerToUpdate = buyerListCache.FirstOrDefault(x => x.BuyerId == b.BuyerId);
                    if (buyerToUpdate != null)
                    {
                        buyerToUpdate.Name = b.Name;
                        buyerToUpdate.Surname = b.Surname;
                        buyerToUpdate.Email = b.Email;
                        buyerToUpdate.PhoneNumber = b.PhoneNumber;
                        buyerToUpdate.IsInLoyaltyProgram = b.IsInLoyaltyProgram;
                    }
                });


            // Act
            await userService.UpdateBuyer(buyerUpdate);

            // Assert
            Assert.NotNull(buyerListDb);
            Assert.Equal(2, buyerListDb.Count);

            Assert.NotNull(buyerListCache);
            Assert.Single(buyerListCache);

            _userRepository.Verify(x => x.UpdateBuyer(It.IsAny<Buyer>()), Times.Once);

            Assert.Equal(buyerListDb[0].Name, buyerUpdate.Name);
            Assert.Equal(buyerListDb[0].IsInLoyaltyProgram, buyerUpdate.IsInLoyaltyProgram);
            Assert.NotEqual(buyerListDb[1].Name, buyerUpdate.Name);

            Assert.Equal(buyerListCache[0].Name, buyerUpdate.Name);
            Assert.Equal(buyerListCache[0].IsInLoyaltyProgram, buyerUpdate.IsInLoyaltyProgram);

        }

        [Fact]
        public async Task Update_Buyer_Failure_NotFound_Test()
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

            Buyer buyerUpdate = new Buyer
            {
                BuyerId = 5,
                Name = "Test500",
                Surname = "TestSurname",
                Email = "TestEmail",
                PhoneNumber = "1234567890",
                IsInLoyaltyProgram = false,

            };

            var buyerListCache = new List<Buyer> { buyer };
            var buyerListDb = new List<Buyer> { buyer, buyer2 };

            _userRepository.Setup(x => x.GetAllBuyers()).ReturnsAsync(buyerListDb);
            _userRepository.Setup(x => x.GetBuyerById(1)).ReturnsAsync(buyer);
            _userRepository.Setup(x => x.UpdateBuyer(It.IsAny<Buyer>()))
                .Callback<Buyer>(b =>
                {
                    var buyerToUpdate = buyerListDb.FirstOrDefault(x => x.BuyerId == b.BuyerId);
                    if (buyerToUpdate != null)
                    {
                        buyerToUpdate.Name = b.Name;
                        buyerToUpdate.Surname = b.Surname;
                        buyerToUpdate.Email = b.Email;
                        buyerToUpdate.PhoneNumber = b.PhoneNumber;
                        buyerToUpdate.IsInLoyaltyProgram = b.IsInLoyaltyProgram;
                    }
                });

            _mongoRepository.Setup(x => x.GetAllBuyers()).ReturnsAsync(buyerListCache);
            _mongoRepository.Setup(x => x.GetBuyerById(1)).ReturnsAsync(buyer);
            _mongoRepository.Setup(x => x.UpdateBuyer(It.IsAny<Buyer>()))
                .Callback<Buyer>(b =>
                {
                    var buyerToUpdate = buyerListCache.FirstOrDefault(x => x.BuyerId == b.BuyerId);
                    if (buyerToUpdate != null)
                    {
                        buyerToUpdate.Name = b.Name;
                        buyerToUpdate.Surname = b.Surname;
                        buyerToUpdate.Email = b.Email;
                        buyerToUpdate.PhoneNumber = b.PhoneNumber;
                        buyerToUpdate.IsInLoyaltyProgram = b.IsInLoyaltyProgram;
                    }
                });


            // Act


            // Assert
            await Assert.ThrowsAsync<Exception>(() => userService.UpdateBuyer(buyerUpdate));
        }

        [Fact]
        public async Task Update_Seller_Success_Test()
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

            Seller sellerUpdate = new Seller
            {
                SellerId = 1,
                Name = "Test500",
                Surname = "TestSurname",
                Email = "TestEmail",
                PhoneNumber = "1234567890",
                Position = SellerPosition.Cashier,

            };


            var sellerListCache = new List<Seller> { seller };
            var sellerListDb = new List<Seller> { seller, seller2 };

            _userRepository.Setup(x => x.GetAllSellers()).ReturnsAsync(sellerListDb);
            _userRepository.Setup(x => x.GetSellerById(1)).ReturnsAsync(seller);
            _userRepository.Setup(x => x.UpdateSeller(It.IsAny<Seller>()))
                .Callback<Seller>(b =>
                {
                    var sellerToUpdate = sellerListDb.FirstOrDefault(x => x.SellerId == b.SellerId);
                    if (sellerToUpdate != null)
                    {
                        sellerToUpdate.Name = b.Name;
                        sellerToUpdate.Surname = b.Surname;
                        sellerToUpdate.Email = b.Email;
                        sellerToUpdate.PhoneNumber = b.PhoneNumber;
                        sellerToUpdate.Position = b.Position;
                    }
                });

            _mongoRepository.Setup(x => x.GetAllSellers()).ReturnsAsync(sellerListCache);
            _mongoRepository.Setup(x => x.GetSellerById(1)).ReturnsAsync(seller);
            _mongoRepository.Setup(x => x.UpdateSeller(It.IsAny<Seller>()))
                .Callback<Seller>(b =>
                {
                    var sellerToUpdate = sellerListCache.FirstOrDefault(x => x.SellerId == b.SellerId);
                    if (sellerToUpdate != null)
                    {
                        sellerToUpdate.Name = b.Name;
                        sellerToUpdate.Surname = b.Surname;
                        sellerToUpdate.Email = b.Email;
                        sellerToUpdate.PhoneNumber = b.PhoneNumber;
                        sellerToUpdate.Position = b.Position;
                    }
                });


            // Act
            await userService.UpdateSeller(sellerUpdate);

            // Assert
            Assert.NotNull(sellerListDb);
            Assert.Equal(2, sellerListDb.Count);

            Assert.NotNull(sellerListCache);
            Assert.Single(sellerListCache);

            _userRepository.Verify(x => x.UpdateSeller(It.IsAny<Seller>()), Times.Once);

            Assert.Equal(sellerListDb[0].Name, sellerUpdate.Name);
            Assert.Equal(sellerListDb[0].Position, sellerUpdate.Position);
            Assert.NotEqual(sellerListDb[1].Name, sellerUpdate.Name);

            Assert.Equal(sellerListCache[0].Name, sellerUpdate.Name);
            Assert.Equal(sellerListCache[0].Position, sellerUpdate.Position);

        }

        [Fact]
        public async Task Update_Seller_Failure_NotFound_Test()
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

            Seller sellerUpdate = new Seller
            {
                SellerId = 5,
                Name = "Test500",
                Surname = "TestSurname",
                Email = "TestEmail",
                PhoneNumber = "1234567890",
                Position = SellerPosition.Cashier,

            };


            var sellerListCache = new List<Seller> { seller };
            var sellerListDb = new List<Seller> { seller, seller2 };

            _userRepository.Setup(x => x.GetAllSellers()).ReturnsAsync(sellerListDb);
            _userRepository.Setup(x => x.GetSellerById(1)).ReturnsAsync(seller);
            _userRepository.Setup(x => x.UpdateSeller(It.IsAny<Seller>()))
                .Callback<Seller>(b =>
                {
                    var sellerToUpdate = sellerListDb.FirstOrDefault(x => x.SellerId == b.SellerId);
                    if (sellerToUpdate != null)
                    {
                        sellerToUpdate.Name = b.Name;
                        sellerToUpdate.Surname = b.Surname;
                        sellerToUpdate.Email = b.Email;
                        sellerToUpdate.PhoneNumber = b.PhoneNumber;
                        sellerToUpdate.Position = b.Position;
                    }
                });

            _mongoRepository.Setup(x => x.GetAllSellers()).ReturnsAsync(sellerListCache);
            _mongoRepository.Setup(x => x.GetSellerById(1)).ReturnsAsync(seller);
            _mongoRepository.Setup(x => x.UpdateSeller(It.IsAny<Seller>()))
                .Callback<Seller>(b =>
                {
                    var sellerToUpdate = sellerListCache.FirstOrDefault(x => x.SellerId == b.SellerId);
                    if (sellerToUpdate != null)
                    {
                        sellerToUpdate.Name = b.Name;
                        sellerToUpdate.Surname = b.Surname;
                        sellerToUpdate.Email = b.Email;
                        sellerToUpdate.PhoneNumber = b.PhoneNumber;
                        sellerToUpdate.Position = b.Position;
                    }
                });


            // Act

            // Assert
            await Assert.ThrowsAsync<Exception>(() => userService.UpdateSeller(sellerUpdate));

        }

    }

}