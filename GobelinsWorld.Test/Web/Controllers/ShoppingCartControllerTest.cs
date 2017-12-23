namespace GobelinsWorld.Test.Web.Controllers
{
    using FluentAssertions;
    using GobelinsWorld.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ShoppingCartControllerTest
    {
        [Fact]
        public void FinishShouldBeOnlyForAutorizedUsers()
        {
            //Arrange
            var method = typeof(ShoppingCartController).GetMethod(nameof(ShoppingCartController.Finish));

            //Act
            var attributes = method.GetCustomAttributes(true);

            //Assert
            attributes.Should()
                .Match(attr => attr.Any(a => a.GetType() == typeof(AuthorizeAttribute)));
        }
               
    }
}
