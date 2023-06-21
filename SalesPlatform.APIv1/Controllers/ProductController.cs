using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SalesPlatform.APIv1.ViewModel;
using SalesPlatform.Domain;
using SalesPlatform.Domain.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SalesPlatform.APIv1.Controllers
{
    /// <summary>
    /// Controlador.
    /// </summary>
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> logger;
        private readonly IProductsRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="logger">Objecto log.</param>
        /// <param name="queries">Objecto para repositorio.</param>
        public ProductController(ILogger<ProductController> logger, IProductsRepository queries)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.repository = queries ?? throw new ArgumentNullException(nameof(queries));
        }

        /// <summary>
        /// Permite crear un nuevo producto.
        /// </summary>
        [HttpPost]
        [SwaggerOperation("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] Product command)
        {
            if (command == null)
            {
                return this.UnprocessableEntity();
            }

            this.repository.Add(command);

            return this.Ok();
        }

        /// <summary>
        /// Permite eliminar un producto.
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerOperation("DeleteProduct")]
        public IActionResult DeleteProduct(Guid id)
        {
            var result = this.repository.FindByIdAsync(id);
            if (result == null)
            {
                return this.NotFound();
            }

            this.repository.Delete(result);

            return this.Ok();
        }

        /// <summary>
        /// Permite modificar un producto.
        /// </summary>
        [HttpPut]
        [SwaggerOperation("UpdateProduct")]
        public IActionResult UpdateProduct([FromBody] ProductVM command)
        {
            if (command == null)
            {
                return this.UnprocessableEntity();
            }

            var result = this.repository.FindByIdAsync(command.Id);
            if (result == null)
            {
                return this.NotFound();
            }

            result.Name = command.Name;
            result.CostAmount = command.CostAmount;

            this.repository.Update(result);

            return this.Ok();
        }

        /// <summary>
        /// Buscar todos los productos disponibles.
        /// </summary>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
        [SwaggerOperation("GetAllProduct")]
        public IActionResult GetAll()
        {
            var result = this.repository.GetAllAsync();
            if (result != null)
            {
                return this.Ok(result);
            }
            else
            {
                return this.NotFound();
            }
        }

        /// <summary>
        /// Busca el producto
        /// </summary>
        /// <param name="name">Nombre por el que buscar.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [SwaggerOperation("GetProductById")]
        public IActionResult GetByName(Guid id)
        {
            var result = this.repository.FindByIdAsync(id);
            if (result != null)
            {
                return this.Ok(result);
            }
            else
            {
                return this.NotFound();
            }
        }
    }
}
