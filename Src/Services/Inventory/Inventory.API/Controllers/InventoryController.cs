using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Inventory.API.Common;
using Inventory.API.Entities;
using Inventory.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inventory.API.Controllers
{
    [Route("v1")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<InventoryController> _logger;
        private Response response;

        public InventoryController(IProductRepository repository, ILogger<InventoryController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            response = new Response(string.Empty);
        }

        /// <summary>
        /// Get all all products. /v1/products GET
        /// </summary>
        /// <returns></returns>
        [Route("products")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> Products()
        {
            try
            {
                var products = await _repository.GetProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(Constants.ServerError);
            }
        }

        /// <summary>
        /// Get product by id. /v1/product/{id} GET
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("product/{id}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> Product(string id)
        {
            try
            {
                var product = await _repository.GetProduct(id);
                if (product == null)
                {
                    response.message = string.Format(Constants.ProductNotFound, id);
                    return NotFound(response);
                }
                return Ok(product);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return Problem(Constants.ServerError);
            }
        }

        /// <summary>
        /// Create a single product. /v1/product POST
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [Route("product")]
        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromForm] Product product)
        {
            try
            {
                return Ok(await _repository.Create(product));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(Constants.ServerError);
            }
        }

        /// <summary>
        /// Update a single product. /v1/product/{id} PUT
        /// </summary>
        /// <param name="value"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("product/{id}")]
        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromForm] Product value, string id)
        {
            try
            {
                var product = await _repository.GetProduct(id);
                if (product == null)
                {
                    response.message = string.Format(Constants.ProductNotFound, id);
                    return NotFound(response);
                }
                value.Id = product.Id;
                value.Name = value.Name ?? product.Name;
                value.Price = value.Price ?? product.Price;
                await _repository.Update(value);
                return Ok(value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(Constants.ServerError);
            }
        }

        /// <summary>
        /// Delete a single product. /v1/product/{id} DELETE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("product/{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            try
            {
                var product = await _repository.GetProduct(id);
                if (product == null)
                {
                    response.message = string.Format(Constants.ProductNotFound, id);
                    return NotFound(response);
                }
                await _repository.Delete(product.Id);
                response.message = string.Format(Constants.ProductDeleted, id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(Constants.ServerError);
            }
        }
    }
}
