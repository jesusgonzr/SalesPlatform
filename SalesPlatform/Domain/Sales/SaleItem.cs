using SalesPlatform.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesPlatform.Domain.Sales
{
    /// <summary>
    /// Clase que contien una venta.
    /// </summary>
    public class SaleItem : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaleItem"/> class.
        /// </summary>
        public SaleItem()
        {
        }

        /// <summary>
        /// Producto id.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Precio
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets relation id.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Gets or sets relation object.
        /// </summary>
        public Sale Sale { get; set; }
    }
}
