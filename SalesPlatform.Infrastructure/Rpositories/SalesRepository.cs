using Microsoft.EntityFrameworkCore;
using SalesPlatform.Domain;
using SalesPlatform.Domain.Interfaces;
using SalesPlatform.Domain.Sales;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesPlatform.Infrastructure.Rpositories
{
    public class SalesRepository : ISalesRepository
    {
        private readonly SalesPlatformContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesRepository"/> class.
        /// </summary>
        /// <param name="context">Contexto de la base de datos.</param>
        public SalesRepository(SalesPlatformContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Sale sale)
        {
            this.context.Sales.Add(sale);
            this.context.SaveChanges();
        }

        public void Delete(Sale sale)
        {
            var data = this.context.Sales.Where(o => o.Id == sale.Id).FirstOrDefault();
            this.context.Sales.Remove(data);
            this.context.SaveChanges();
        }

        public Sale FindByIdAsync(Guid id)
        {
            return this.context.Sales.Where(o => o.Id == id).FirstOrDefault();
        }

        public List<Sale> GetAllAsync()
        {
            return this.context.Sales.ToList();
        }

        public void Update(Sale sale)
        {
            using (var transaction = this.context.Database.BeginTransaction())
            {
                try
                {

                    if (sale.SaleItems != null && sale.SaleItems.Any())
                    {
                        foreach (var item in sale.SaleItems)
                        {
                            // Check if there are already records in database.
                            var search = this.context.Sales
                                .Where(p => p.Id == item.Id)
                                .FirstOrDefault();

                            // Add new value.
                            if (search == null)
                            {
                                this.context.SalesItems.Add(item);
                            }
                        }
                    }

                    var exist = this.context.Sales.Find(sale.Id);
                    this.context.Entry(exist).CurrentValues.SetValues(sale);

                    this.context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
