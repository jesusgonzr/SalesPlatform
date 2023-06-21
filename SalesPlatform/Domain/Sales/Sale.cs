using SalesPlatform.Domain.Commons;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesPlatform.Domain.Sales
{
    /// <summary>
    /// Clase que contien una venta.
    /// </summary>
    public class Sale : Entity, IAggregateRoot
    {
        // DDD Patterns comment
        // Using a private collection field, better for DDD Aggregate's encapsulation
        // so addressItems cannot be added from "outside the AggregateRoot" directly to the collection.
        private List<SaleItem> saleItems;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sale"/> class.
        /// </summary>
        public Sale() 
        {
            this.saleItems = new List<SaleItem>();
        }

        /// <summary>
        /// Fecha de la venta
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Precio total.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets process file items.
        /// </summary>
        public IList<SaleItem> SaleItems => this.saleItems;

        public void AddSaleItem(SaleItem item)
        {
            this.saleItems.Add(item);
        }
    }
}
