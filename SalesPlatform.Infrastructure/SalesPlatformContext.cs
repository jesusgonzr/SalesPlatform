using Microsoft.EntityFrameworkCore;
using SalesPlatform.Domain;
using SalesPlatform.Domain.Sales;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Emit;

namespace SalesPlatform.Infrastructure
{
    /// <summary>
    /// Process context class.
    /// </summary>
    public class SalesPlatformContext : DbContext
    {
        /// <summary>
        /// Default schema.
        /// </summary>
        public const string DEFAULTSCHEMA = "dbo";

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesPlatformContext"/> class.
        /// </summary>
        /// <param name="options">DbContext options.</param>
        public SalesPlatformContext(DbContextOptions<SalesPlatformContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets Produt table.
        /// </summary>
        public virtual DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets Sale table.
        /// </summary>
        public virtual DbSet<Sale> Sales { get; set; }

        /// <summary>
        /// Gets or sets SalesItems table.
        /// </summary>
        public virtual DbSet<SaleItem> SalesItems { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}