using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.SeedData
{
    public class GenerateFakeData
    {
        public static async Task SeedDataAsync(ApplicationDbContext context , ILoggerFactory loggerfactory)
        {
            try
            {
                if (!await context.productbrand.AnyAsync())
                {
                    var brands = new List<ProductBrand>()
                    {
                        new()
                        {
                            Description = "product brand 1" ,
                            Summary = "summery 1",
                            Title = "brand 1",
                        },
                        new ()
                        {
                            Description = "product brand 2" ,
                            Summary = "summery 2",
                            Title = "brand 2",
                        }
                    };
                    await context.productbrand.AddRangeAsync(brands);
                    context.SaveChanges();
                }
                if (!await context.producttype.AnyAsync())
                {
                    var types = new List<ProductType>()
                    {
                        new()
                        {
                            Description = "product type 1" ,
                            Summary = "summery 1",
                            Title = "type 1",
                        },
                        new ()
                        {
                            Description = "product type 2" ,
                            Summary = "summery 2",
                            Title = "type 2",
                        }
                    };
                    await context.producttype.AddRangeAsync(types);
                    context.SaveChanges();
                }

                if (!await context.products.AnyAsync())
                {
                    var products = new List<Product>()
                    {
                    new()
                    {
                        Description = "test",
                        Price=15000,
                        Title ="test" ,
                        Summary = "summery test",
                        PictureUrl="",
                        ProductTypeId =1 ,
                        ProductBrandId = 1,
                    }
                    };
                    await context.products.AddRangeAsync(products);
                    context.SaveChanges();
                }

            }
            catch (Exception e)
            {
                var logger = loggerfactory.CreateLogger<GenerateFakeData>();
                logger.LogError(e, "error in seed data");
            }
           
        }
    }
}
