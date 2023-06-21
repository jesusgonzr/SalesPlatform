using Microsoft.EntityFrameworkCore;
using SalesPlatform.Domain;
using SalesPlatform.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesPlatform.Infrastructure.Rpositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly SalesPlatformContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsRepository"/> class.
        /// </summary>
        /// <param name="context">Contexto de la base de datos.</param>
        public ProductsRepository(SalesPlatformContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Product product)
        {
            this.context.Products.Add(product);
            this.context.SaveChanges();
        }

        public void Delete(Product product)
        {
            var data = this.context.Products.Where(o => o.Id == product.Id).FirstOrDefault();
            this.context.Products.Remove(data);
            this.context.SaveChanges();
        }

        public Product FindByIdAsync(Guid id)
        {
            return this.context.Products.Where(o => o.Id == id).FirstOrDefault();
        }

        public List<Product> GetAllAsync()
        {
            return this.context.Products.ToList();
        }

        public void Update(Product Product)
        {
            try
            {
                var exist = this.context.Products.Find(Product.Id);
                this.context.Entry(exist).CurrentValues.SetValues(Product);

                this.context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
