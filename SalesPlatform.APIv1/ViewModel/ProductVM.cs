namespace SalesPlatform.APIv1.ViewModel
{
    public class ProductVM
    {
        /// <summary>
        /// Identificador del produto.
        /// </summary>
        public Guid Id { get; set; }

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
