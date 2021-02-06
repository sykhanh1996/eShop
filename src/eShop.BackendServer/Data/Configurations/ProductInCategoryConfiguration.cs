using eShop.BackendServer.Data.Entities;
using eShop.BackendServer.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.BackendServer.Data.Configurations
{
    public class ProductInCategoryConfiguration : DbEntityConfiguration<ProductInCategory>
    {
        public override void Configure(EntityTypeBuilder<ProductInCategory> entity)
        {
            entity.HasKey(e => new { e.CategoryId, e.ProductId });
            // etc.
        }
    }
}