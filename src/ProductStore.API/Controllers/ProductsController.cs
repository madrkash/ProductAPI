using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductStore.API.ApiModels;
using ProductStore.API.Exceptions;
using ProductStore.Core.Contracts;
using ProductStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProductStore.API.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("V{version:apiVersion}/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductsController(ILogger<ProductsController> logger,
            IProductService service,
            IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                _logger.LogDebug($"Received {nameof(GetAllProducts)} request");
                var products = await _service.GetAllAsync();
                _logger.LogDebug($"Returned {products.Count()} products as part of " +
                                 $"{nameof(GetAllProducts)} request");
                return Ok(_mapper.Map<IEnumerable<ProductViewModel>>(products));
            }
            catch (EntityNotFoundException exception)
            {
                _logger.LogError(exception, exception.Message);
                return NotFound(exception.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ProductViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                _logger.LogDebug($"Received {nameof(GetProductById)} request with Id {id}");
                var product = await _service.GetByIdAsync(id);
                _logger.LogDebug($"Returned {nameof(GetProductById)} response with product " +
                                 $"{JsonConvert.SerializeObject(product)}");

                return Ok(_mapper.Map<ProductViewModel>(product));
            }
            catch (ProductNotFoundException exception)
            {
                _logger.LogError(exception, exception.Message);
                return NotFound(exception.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddProduct(ProductCreateRequest productCreateRequest)
        {
            _logger.LogDebug($"Received {nameof(AddProduct)} request with " +
                             $"{JsonConvert.SerializeObject(productCreateRequest)}");
            var productId = await _service.AddAsync(_mapper.Map<Product>(productCreateRequest));
            _logger.LogDebug($"Returned {nameof(AddProduct)} request with Id {productId}");

            return CreatedAtAction(nameof(AddProduct), new { id = productId }, productId);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> UpdateProduct(ProductUpdateRequest productUpdateRequest)
        {
            try
            {
                _logger.LogDebug($"Received {nameof(UpdateProduct)} request with " +
                                 $"{JsonConvert.SerializeObject(productUpdateRequest)}");
                await _service.UpdateAsync(_mapper.Map<Product>(productUpdateRequest));
                _logger.LogDebug($"Returned {nameof(UpdateProduct)} response for Id " +
                                 $"{productUpdateRequest.Id}");
                return CreatedAtAction(nameof(UpdateProduct), new { id = productUpdateRequest.Id }, productUpdateRequest.Id);
            }
            catch (ProductNotFoundException exception)
            {
                _logger.LogError(exception, exception.Message);
                return NotFound(exception.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                _logger.LogDebug($"Received {nameof(DeleteProduct)} request with Id {id}");
                await _service.DeleteAsync(id);
                _logger.LogDebug($"Returned {nameof(DeleteProduct)} response for Id {id}");

                return Accepted();
            }
            catch (ProductNotFoundException exception)
            {
                _logger.LogError(exception, exception.Message);
                return NotFound(exception.Message);
            }
        }
    }
}