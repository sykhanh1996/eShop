using eShop.BackendServer.Data.Entities;
using eShop.BackendServer.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.BackendServer.Data.Configurations
{
    public class FunctionTranslationConfiguration : DbEntityConfiguration<FunctionTranslation>
    {
        public override void Configure(EntityTypeBuilder<FunctionTranslation> entity)
        {
            entity.HasKey(e => new { e.LanguageId, e.FunctionId });
            // etc.
        }
    }
}