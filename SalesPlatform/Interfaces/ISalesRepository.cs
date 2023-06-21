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
    public interface ISalesRepository : IRepository<Sale>
    {
        /// <summary>
        /// Añade un objeto.
        /// </summary>
        /// <param name="sale">Parametro que contiene el objeto nuevo.</param>
        /// <returns>File object.</returns>
        void Add(Sale sale);

        /// <summary>
        /// Modifica un objeto.
        /// </summary>
        /// <param name="sale">Parametro que contiene el objeto a modificar.</param>
        /// <returns>Devuelve el objeto modificado.</returns>
        void Update(Sale sale);

        /// <summary>
        /// Borra un objeto.
        /// </summary>
        /// <param name="sale">Objeto que se va a eliminar.</param>
        void Delete(Sale sale);

        /// <summary>
        /// Se busca un objeto por el identificador.
        /// </summary>
        /// <param name="id">Id param.</param>
        /// <returns>Devuelve el objeto encontrado.</returns>
        Sale FindByIdAsync(Guid id);

        /// <summary>
        /// Devuelve todos los resultados de la base de datos
        /// </summary>
        /// <returns>Devuelve el objeto encontrado.</returns>
        List<Sale> GetAllAsync();
    }
}
