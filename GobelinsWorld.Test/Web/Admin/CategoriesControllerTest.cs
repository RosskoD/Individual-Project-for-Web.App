namespace GobelinsWorld.Test.Web.Admin
{
    using FluentAssertions;
    using GobelinsWorld.Data;
    using GobelinsWorld.Services.Admin;
    using GobelinsWorld.Services.Admin.Models;
    using GobelinsWorld.Web;
    using GobelinsWorld.Web.Areas.Admin.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class CategoriesControllerTest
    {
        [Fact]
        public void CategoriesControllerShouldBeInAdminArea()
        {
            //Arrange
            var controller = typeof(CategoriesController);

            //Act
            var areaAttribute = controller.GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AreaAttribute))
                as AreaAttribute;

            //Assert
            areaAttribute.Should().NotBeNull();
            areaAttribute.RouteValue.Should().Be("Admin");
        }

        [Fact]
        public void CategoriesControllerShouldBeOnlyForAdminUsers()
        {
            //Arrange
            var controller = typeof(CategoriesController);

            //Act
            var areaAttribute = controller.GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AuthorizeAttribute))
                as AuthorizeAttribute;

            //Assert
            areaAttribute.Should().NotBeNull();
            areaAttribute.Roles.Should().Be(WebConstants.AdministratorRole);
        }

        [Fact]
        public void GetCreateShouldReturnViewWithValidModel()
        {
            //Arrange
            var db = this.GetDatabase();

            var categoriesService = new Mock<CategoryService>(db);
            var controller = new CategoriesController(categoriesService.Object);

            //Act
            var result = controller.Create();

            //Assert
            result.Should().BeOfType<ViewResult>();
            var model = result.As<ViewResult>().Model;
            model.Should().BeOfType<CategoryFormServiceModel>();
        }

        [Fact]
        public async Task PostCreateShouldReturnCorrectModelWhenModelStateIsInvalid()
        {
            //Arrange
            var db = this.GetDatabase();

            var categoriesService = new Mock<CategoryService>(db);
            var controller = new CategoriesController(categoriesService.Object);
            controller.ModelState.AddModelError(string.Empty, "Error");

            //Act
            var result = await controller.Create(new CategoryFormServiceModel());

            //Assert
            result.Should().BeOfType<ViewResult>();
            var model = result.As<ViewResult>().Model;
            model.Should().BeOfType<CategoryFormServiceModel>();
            var formModel = model.As<CategoryFormServiceModel>();
        }

        //[Fact]
        //public async Task PostCreateShouldReturnRedirectWithValidModel()
        //{
        //    //Arrange
        //    var db = this.GetDatabase();

        //    string modelName=null;
        //    var categoriesService = new Mock<CategoryService>(db);
        //    categoriesService.Setup(c => c.Create(It.IsAny<string>()))
        //        .Callback((string name)=> 
        //    {
        //        modelName = name;
        //    })
        //    .Returns(Task.CompletedTask);

        //    string successMessage = null;
        //    var tempData = new Mock<ITempDataDictionary>();
        //    tempData
        //        .SetupSet(t => t["SuccessMessage"] == It.IsAny<string>())
        //        .Callback((string key, object message) => successMessage = message as string);
        //    var controller = new CategoriesController(categoriesService.Object);
        //    controller.ModelState.AddModelError(string.Empty, "Error");
        //    controller.TempData = tempData.Object;
        //    //Act
        //    var result = await controller.Create(new CategoryFormServiceModel()
        //    {
        //        Name ="NewName"
        //    });

        //    //Assert
        //    modelName.Should().Be("NewName");
        //    successMessage.Should().Be($"Category NewName created successfully.");
        //    result.Should().BeOfType<RedirectToActionResult>();
        //    result.As<RedirectToActionResult>().ActionName.Should().Be("All");
        //    result.As<RedirectToActionResult>().ControllerName.Should().Be("Categories");
        //    result.As<RedirectToActionResult>().RouteValues.Keys.Should().Contain("area");
        //    result.As<RedirectToActionResult>().RouteValues.Values.Should().Contain("Admin");
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