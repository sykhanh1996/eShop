using eShop.BackendServer.Data.Entities;
using eShop.BackendServer.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.BackendServer.Data.Configurations
{
    public class CategoryTranslationConfiguration : DbEntityConfiguration<CategoryTranslation>
    {
        public override void Configure(EntityTypeBuilder<CategoryTranslation> entity)
        {
            entity.HasKey(e => new { e.LanguageId, e.CategoryId });
            // etc.
        }
    }
}