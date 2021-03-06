﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using eShop.BackendServer.Data.Configurations;
using eShop.BackendServer.Data.Entities;
using eShop.BackendServer.Data.Extensions;
using eShop.BackendServer.Data.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eShop.BackendServer.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public string UserId { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ActivityLog> ActivityLogs { set; get; }
        public DbSet<Attachment> Attachments { set; get; }
        public DbSet<Command> Commands { set; get; }
        public DbSet<CommandInFunction> CommandInFunctions { set; get; }
        public DbSet<Function> Functions { set; get; }
        public DbSet<Permission> Permissions { set; get; }
        public DbSet<FunctionTranslation> FunctionTranslations { set; get; }
        public DbSet<AttibuteValueText> AttibuteValueTexts { set; get; }
        public DbSet<Attributes> Attributes { set; get; }
        public DbSet<AttributeOption> AttributeOptions { set; get; }
        public DbSet<AttributeOptionValue> AttributeOptionValues { set; get; }
        public DbSet<AttributeValueDateTime> AttributeValueDateTimes { set; get; }
        public DbSet<AttributeValueDecimal> AttributeValueDecimals { set; get; }
        public DbSet<AttributeValueInt> AttributeValueInts { set; get; }
        public DbSet<AttributeValueVarchar> AttributeValueVarchars { set; get; }
        public DbSet<Bill> Bills { set; get; }
        public DbSet<BillDetail> BillDetails { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<CategoryTranslation> CategoryTranslations { set; get; }
        public DbSet<Language> Languages { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<ProductInCategory> ProductInCategories { set; get; }
        public DbSet<ProductTranslation> ProductTranslations { set; get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Identity Configuration

            builder.Entity<AppRole>().Property(x => x.Id).HasMaxLength(50).IsUnicode(false);
            builder.Entity<User>().Property(x => x.Id).HasMaxLength(50).IsUnicode(false);


            builder.Entity<Language>().Property(x => x.SortOrder).HasDefaultValue(0);
            builder.Entity<Language>().Property(x => x.IsDefault).HasDefaultValue(false);
            builder.Entity<AttributeOption>().Property(x => x.SortOrder).HasDefaultValue(0);
            builder.Entity<Attributes>().Property(x => x.SortOrder).HasDefaultValue(0);
            builder.Entity<Bill>().Property(x => x.SortOrder).HasDefaultValue(0);
            builder.Entity<Category>().Property(x => x.SortOrder).HasDefaultValue(0);
            builder.Entity<Language>().Property(x => x.SortOrder).HasDefaultValue(0);
            builder.Entity<Category>().Property(x => x.SortOrder).HasDefaultValue(0);



            #endregion Identity Configuration

            builder.AddConfiguration(new BillDetailConfiguration());
            builder.AddConfiguration(new CategoryTranslationConfiguration());
            builder.AddConfiguration(new CommandInFunctionConfiguration());
            builder.AddConfiguration(new FunctionTranslationConfiguration());
            builder.AddConfiguration(new PermissionConfiguration());
            builder.AddConfiguration(new ProductInCategoryConfiguration());
            builder.AddConfiguration(new ProductTranslationConfiguration());
            builder.AddConfiguration(new ProductConfiguration());

            builder.HasSequence("ProductSequence");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            IEnumerable<EntityEntry> modified = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
            foreach (EntityEntry item in modified)
            {
                if (item.Entity is IDateTracking changedOrAddedItem)
                {
                    if (item.State == EntityState.Added)
                    {
                        changedOrAddedItem.CreateDate = DateTime.Now;
                        if (!string.IsNullOrEmpty(UserId)) changedOrAddedItem.CreatedBy = UserId;
                    }
                    else
                    {
                        changedOrAddedItem.LastModifiedDate = DateTime.Now;
                        if (!string.IsNullOrEmpty(UserId)) changedOrAddedItem.ModifiedBy = UserId;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}