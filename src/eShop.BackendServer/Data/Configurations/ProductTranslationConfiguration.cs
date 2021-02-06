using eShop.BackendServer.Data.Entities;
using eShop.BackendServer.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.BackendServer.Data.Configurations
{
    public class ProductTranslationConfiguration : DbEntityConfiguration<ProductTranslation>
    {
        public override void Configure(EntityTypeBuilder<ProductTranslation> entity)
        {
            entity.HasKey(e => new { e.LanguageId, e.ProductId });
            // etc.
        }
    }
}