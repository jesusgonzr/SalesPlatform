using SalesPlatform.Domain.Commons;
using SalesPlatform.Domain.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesPlatform.Domain.Interfaces
{
    /// <summary>
    ///  This is just the repository contracts or interface defined at the Domain Layer
    ///  as requisite for the Templates Aggregate.
    /// </summary>
    public interface IProductsRepository : IRepository<Product>
    {
        /// <summary>
        /// Añade un objeto.
        /// </summary>
        /// <param name="product">Parametro que contiene el objeto nuevo.</param>
        /// <returns>File object.</returns>
        void Add(Product product);

        /// <summary>
        /// Modifica un objeto.
        /// </summary>
        /// <param name="Product">Parametro que contiene el objeto a modificar.</param>
        /// <returns>Devuelve el objeto modificado.</returns>
        void Update(Product product);

        /// <summary>
        /// Borra un objeto.
        /// </summary>
        /// <param name="Product">Objeto que se va a eliminar.</param>
        void Delete(Product product);

        /// <summary>
        /// Se busca un objeto por el identificador.
        /// </summary>
        /// <param name="id">Id param.</param>
        /// <returns>Devuelve el objeto encontrado.</returns>
        Product FindByIdAsync(Guid id);

        /// <summary>
        /// Devuelve todos los resultados de la base de datos
        /// </summary>
        /// <returns>Devuelve el objeto encontrado.</returns>
        List<Product> GetAllAsync();
    }
}
