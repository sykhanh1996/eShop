using eShop.BackendServer.Data.Entities;
using eShop.BackendServer.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.BackendServer.Data.Configurations
{
    public class BillDetailConfiguration : DbEntityConfiguration<BillDetail>
    {
        public override void Configure(EntityTypeBuilder<BillDetail> entity)
        {
            entity.HasKey(b => new { b.ProductId, b.BillId });
            entity.Property(x => x.Price).HasDefaultValue(0);
            // etc.
        }
    }
}