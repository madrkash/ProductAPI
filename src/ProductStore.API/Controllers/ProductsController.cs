using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductStore.Core.Contracts;
using ProductStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ProductStore.API.Dtos;
using ProductStore.Core.Exceptions;

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
        [ProducesResponseType(typeof(IEnumerable<ProductResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                _logger.LogDebug($"Received {nameof(GetAllProducts)} request");
                var products = await _service.GetAllAsync();
                _logger.LogDebug($"Returned {products.Count()} products as part of {nameof(GetAllProducts)} request");
                return Ok(_mapper.Map<IEnumerable<ProductResponseDto>>(products));
            }
            catch (EntityNotFoundException exception)
            {
                _logger.LogError(exception, exception.Message);
                return NotFound(exception.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ProductResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                _logger.LogDebug($"Received {nameof(GetProductById)} request with Id {id}");
                var product = await _service.GetByIdAsync(id);
                _logger.LogDebug($"Returned {nameof(GetProductById)} response with product {{@Product}}", product);
                return Ok(_mapper.Map<ProductResponseDto>(product));
            }
            catch (ProductNotFoundException exception)
            {
                _logger.LogError(exception, exception.Message);
                return NotFound(exception.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddProduct(ProductCreateRequestDto productCreateRequest)
        {
            _logger.LogDebug($"Received {nameof(AddProduct)} request with {{@ProductCreateRequest}}", productCreateRequest);
            var productId = await _service.AddAsync(_mapper.Map<Product>(productCreateRequest));
            _logger.LogDebug($"Returned {nameof(AddProduct)} request with Id {productId}");

            return CreatedAtAction(nameof(AddProduct), new { id = productId }, productId);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductUpdateRequestDto productUpdateRequest)
        {
            try
            {
                _logger.LogDebug($"Received {nameof(UpdateProduct)} request with {{@ProductUpdateRequest}}", productUpdateRequest);
                
                if (id != productUpdateRequest.Id)
                {
                    return BadRequest("Parameter mismatch between the route and the payload");
                }

                await _service.UpdateAsync(_mapper.Map<Product>(productUpdateRequest));
                _logger.LogDebug($"Returned {nameof(UpdateProduct)} response for Id {productUpdateRequest.Id}");
                return CreatedAtAction(nameof(UpdateProduct), 
                    new { id = productUpdateRequest.Id }, 
                    productUpdateRequest.Id);
            }
            catch (ProductNotFoundException exception)
            {
                _logger.LogError(exception, exception.Message);
                return NotFound(exception.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
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