using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;

namespace Persistence.Data.DataSeeding
{
    public class DbIntializer : IDbIntialaizer
    {
        private readonly ApplicationDbContext context;

        public DbIntializer(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task IntializeAsync()
        {
            try
            {
                if (!(await context.Database.GetPendingMigrationsAsync()).Any())
                {
                    await context.Database.MigrateAsync();
                    //
                    if (!context.ProductBrands.Any())
                    {
                        var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeeding\JsonData\brands.json");
                        var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                        if (brands is not null && brands.Any())
                        {
                            await context.AddRangeAsync(brands);
                            await context.SaveChangesAsync();
                        }
                    }
                    //
                    if (!context.ProductTypes.Any())
                    {
                        var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeeding\JsonData\types.json");
                        var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                        if (types is not null && types.Any())
                        {
                            await context.AddRangeAsync(types);
                            await context.SaveChangesAsync();
                        }
                    }
                    //
                    if (!context.Products.Any())
                    {
                        var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeeding\JsonData\products.json");
                        var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                        if (products is not null && products.Any())
                        {
                            await context.AddRangeAsync(products);
                            await context.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (Exception ex )
            {

            }
        }
    }
}
