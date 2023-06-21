using SalesPlatform.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesPlatform.Domain
{
    /// <summary>
    /// Clase que contien un producto.
    /// </summary>
    public class Product : Entity, IAggregateRoot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        public Product()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Nombre del producto.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Coste del producto.
        /// </summary>
        public decimal CostAmount { get; set; }
    }
}
