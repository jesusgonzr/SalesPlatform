using SalesPlatform.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesPlatform.APIv1.ViewModel
{
    /// <summary>
    /// Clase que contien una venta.
    /// </summary>
    public class SaleItemVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaleItem"/> class.
        /// </summary>
        public SaleItemVM()
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
    }
}
