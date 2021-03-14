using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.BackendServer.Data.Entities;
using eShop.BackendServer.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.BackendServer.Data.Configurations
{
    public class ProductConfiguration: DbEntityConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> entity)
        {
           entity.Property(x => x.Price).HasDefaultValue(0);
           entity.Property(x => x.OriginalPrice).HasDefaultValue(0);
           entity.Property(x => x.ViewCount).HasDefaultValue(0);
            // etc.
        }
    }
}
