namespace GobelinsWorld.Test.Services
{
    using Data;
    using Data.Models;
    using FluentAssertions;
    using GobelinsWorld.Services.User;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class UserProductsServiceTest
    {
        public UserProductsServiceTest()
        {
            TestStartup.Initialize();
        }

        [Fact]
        public async Task NewShouldReturnLastNineProductsInDescendingOrder()
        {
            //Arrange  
            var db = this.GetDatabase();

            var firstProduct = new Product { Id = 1, Name = "FirstProduct" };
            var secondProduct = new Product { Id = 2, Name = "secondProduct" };
            var thirdProduct = new Product { Id = 3, Name = "thirdProduct" };
            var fourthProduct = new Product { Id = 4, Name = "fourthProduct" };
            var fifthProduct = new Product { Id = 5, Name = "fifthProduct" };
            var sixthProduct = new Product { Id = 6, Name = "sixthProduct" };
            var seventhProduct = new Product { Id = 7, Name = "seventhProduct" };
            var eighthProduct = new Product { Id = 8, Name = "eighthProduct" };
            var ninthProduct = new Product { Id = 9, Name = "ninthProduct" };
            var tenthsProduct = new Product { Id = 10, Name = "tenthsProduct" };

            db.AddRange(firstProduct, secondProduct, thirdProduct, fourthProduct, fifthProduct, sixthProduct, seventhProduct, eighthProduct, ninthProduct, tenthsProduct);

            await db.SaveChangesAsync();

            var userProductService = new UserProductService(db);

            //Act
            var result = await userProductService.New();

            //Assert
            result
                .Should()
                .Match(p => p.ElementAt(0).Id == 10
                && p.ElementAt(8).Id == 2)
                .And
                .HaveCount(9);
        }

        [Fact]
        public async Task AllByCategoryShoudReturnProductByCategoryIdInDescendingOrder()
        {
            //Arrange  
            var db = this.GetDatabase();

            var firstProduct = new Product { Id = 1, Name = "FirstProduct", CategoryId = 1 };
            var secondProduct = new Product { Id = 2, Name = "secondProduct", CategoryId = 2 };
            var thirdProduct = new Product { Id = 3, Name = "thirdProduct", CategoryId = 1 };

            db.AddRange(firstProduct, secondProduct, thirdProduct);
            await db.SaveChangesAsync();

            var userProductService = new UserProductService(db);
            //Act 
            var result =await userProductService.AllByCategory(1);

            //Assert
            result
                 .Should()
                .Match(p => p.ElementAt(0).Id == 3
                && p.ElementAt(1).Id == 1)
                .And
                .HaveCount(2);
        }

        [Fact]
        public async Task AllByProducerShoudReturnProductByProducerIdInDescendingOrder()
        {
            //Arrange  
            var db = this.GetDatabase();

            var firstProduct = new Product { Id = 1, Name = "FirstProduct", ProducerId = 1 };
            var secondProduct = new Product { Id = 2, Name = "secondProduct", ProducerId = 1 };
            var thirdProduct = new Product { Id = 3, Name = "thirdProduct", ProducerId = 2 };

            db.AddRange(firstProduct, secondProduct, thirdProduct);
            await db.SaveChangesAsync();

            var userProductService = new UserProductService(db);
            //Act 
            var result = await userProductService.AllByProducer(1);

            //Assert
            result
                 .Should()
                .Match(p => p.ElementAt(0).Id == 2
                && p.ElementAt(1).Id == 1)
                .And
                .HaveCount(2);
        }

        [Fact]
        public async Task AllBySearchShoudReturnProductBySearchInDescendingOrder()
        {
            //Arrange  
            var db = this.GetDatabase();

            var firstProduct = new Product { Id = 1, Name = "FirstProduct" };
            var secondProduct = new Product { Id = 2, Name = "secondProduct" };
            var thirdProduct = new Product { Id = 3, Name = "thirdProduct" };

            db.AddRange(firstProduct, secondProduct, thirdProduct);
            await db.SaveChangesAsync();

            var userProductService = new UserProductService(db);
            //Act 
            var result = await userProductService.AllBySearch("i");

            //Assert
            result
                 .Should()
                .Match(p => p.ElementAt(0).Id == 3
                && p.ElementAt(1).Id == 1)
                .And
                .HaveCount(2);
        }

        private GobelinsWorldDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<GobelinsWorldDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;

            return new GobelinsWorldDbContext(dbOptions);
        }
    }
}
