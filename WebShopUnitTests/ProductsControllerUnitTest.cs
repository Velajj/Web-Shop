using Core.Abstractions.Services;
using Models.ViewModels;
using Moq;
using WebShop.Controllers;

namespace WebShopUnitTests
{
    [TestClass]
    public class ProductsControllerUnitTest
    {
        [TestMethod]
        public void GetAllProductsTest()
        {
            List<ProductViewModel> productsViewModels = new List<ProductViewModel>
            {
                new ProductViewModel
                {
                    Id = 1,
                    Category = new Domain.Category
                    {
                        Id = 1,
                        Name = "Prehrambeni proizvodi"
                    },
                    Description = "Kafa",
                    Name = "Grand kafa",
                    Price = 200
                }
            };



            var productServiceMock = new Mock<IProductsService>();
            productServiceMock
                .Setup(service => service.GetAllProducts())
                .Returns(productsViewModels);

            var userServiceMock = new Mock<IUsersService>();


            var controller = new ProductsController(productServiceMock.Object, userServiceMock.Object);

            //Act

            var result = controller.GetAllProducts();


            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, productsViewModels.Count);
            Assert.AreEqual(result[0].Id, productsViewModels[0].Id);

        }


        [TestMethod]
        public void GetAllProductsServiceExceptionTest()
        {

            var productServiceMock = new Mock<IProductsService>();
            productServiceMock
                .Setup(service => service.GetAllProducts())
                .Throws(new Exception());

            var userServiceMock = new Mock<IUsersService>();


            var controller = new ProductsController(productServiceMock.Object, userServiceMock.Object);

            //Act

            var result = controller.GetAllProducts();


            Assert.IsNull(result);

        }
    }
}