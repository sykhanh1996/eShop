using eShop.BackendServer.Data.Entities;
using eShop.BackendServer.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.BackendServer.Data.Configurations
{
    public class CommandInFunctionConfiguration : DbEntityConfiguration<CommandInFunction>
    {
        public override void Configure(EntityTypeBuilder<CommandInFunction> entity)
        {
            entity.HasKey(c => new { c.CommandId, c.FunctionId });
            // etc.
        }
    }
}