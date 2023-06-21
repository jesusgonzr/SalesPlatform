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
using SalesPlatform.Domain.Sales;
using Swashbuckle.AspNetCore.Annotations;

namespace SalesPlatform.APIv1.Controllers
{
    /// <summary>
    /// Controlador.
    /// </summary>
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class SalesController : Controller
    {
        private readonly ILogger<SalesController> logger;
        private readonly ISalesRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesController"/> class.
        /// </summary>
        /// <param name="logger">Objecto log.</param>
        /// <param name="queries">Objecto para repositorio.</param>
        public SalesController(ILogger<SalesController> logger, ISalesRepository queries)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.repository = queries ?? throw new ArgumentNullException(nameof(queries));
        }

        /// <summary>
        /// Permite crear una nueva venta.
        /// </summary>
        [HttpPost]
        [SwaggerOperation("CreateSale")]
        public async Task<IActionResult> CreateSale([FromBody] SaleVM command)
        {
            if (command == null)
            {
                return this.UnprocessableEntity();
            }

            var model = new Sale
            {
                Date = command.Date,
                TotalAmount = command.saleItemVMs.Sum(c => c.Amount),
            };
            foreach (var item in command.saleItemVMs)
            {
                model.AddSaleItem(new SaleItem()
                {
                    ProductId = item.ProductId,
                    Amount = item.Amount,
                });
            }
            
            this.repository.Add(model);

            return this.Ok();
        }

        /// <summary>
        /// Permite eliminar una venta.
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerOperation("DeleteSale")]
        public IActionResult DeleteSale(Guid id)
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
        /// Buscar todos las venats disponibles.
        /// </summary>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<Sale>), (int)HttpStatusCode.OK)]
        [SwaggerOperation("GetAllSale")]
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
        /// Busca una venta
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Sale), (int)HttpStatusCode.OK)]
        [SwaggerOperation("GetSaleById")]
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
