namespace GobelinsWorld.Test.Web.Controllers
{
    using FluentAssertions;
    using GobelinsWorld.Data;
    using GobelinsWorld.Data.Models;
    using GobelinsWorld.Services.User;
    using GobelinsWorld.Services.User.Models;
    using GobelinsWorld.Web.Controllers;
    using GobelinsWorld.Web.Models.ProductViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

   public class ProductsControllerTest
    {
        public ProductsControllerTest()
        {
            TestStartup.Initialize();
        }

        [Fact]
        public async Task DetailShouldReturnNotFoundWithInvalidProductId()
        {
            //Arrange
            var db = this.GetDatabase();

            var userProductService =new Mock<UserProductService>(db);
            var controller = new ProductsController(userProductService.Object,null,null);

            //Act
            var result = await controller.Detail(1);

            //Assert
            result
                .Should()
                .BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DetailShouldReturnViewWithCorrectModelWithValidProductId()
        {
            //Arrange
            var db = this.GetDatabase();
          

            var userProductService = new Mock<UserProductService>(db);
            userProductService.Setup(p => p.Detail(It.IsAny<int>())).ReturnsAsync( new ProductDetailServiceModel());
            var controller = new ProductsController(userProductService.Object, null, null);

            //Act
            var result = await controller.Detail(1);

            //Assert
            result
                .Should()
                .BeOfType<ViewResult>()
                .Subject
                .Model
                .Should()
                .Match(m=>m.As<ProductDetailViewModel>().Product.Id==1);
        }

        //[Fact]
        //public async Task SearchShouldRetirnNoResultWithNocriteria()
        //{
        //    //Arrange
        //    var db = this.GetDatabase();


        //    var userProductService = new Mock<UserProductService>(db);
        //    userProductService.Setup(p => p.AllBySearch(It.IsAny<string>(),1,1)).ReturnsAsync( new List<UserProductListingServiceModel>());
        //    var controller = new ProductsController(userProductService, null, null);
        //    //Act
        //    var result = await controller.Search("");
        //    //Assert
        //    result.Should().BeOfType<ViewResult>();
        //    result.As<ViewResult>().Model.Should().BeOfType<UserProductListingServiceModel>();
           
        //}

        private GobelinsWorldDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<GobelinsWorldDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;

            return new GobelinsWorldDbContext(dbOptions);
        }

    }
}
