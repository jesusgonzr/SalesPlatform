using SalesPlatform.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesPlatform.APIv1.ViewModel
{
    /// <summary>
    /// Clase que contien una venta.
    /// </summary>
    public class SaleVM 
    {

        /// <summary>
        /// Fecha de la venta
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets items.
        /// </summary>
        public List<SaleItemVM> saleItemVMs { get; set; }
    }
}
