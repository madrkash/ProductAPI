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
    public class ProductOptionsController : ControllerBase
    {
        private readonly ILogger<ProductOptionsController> _logger;
        private readonly IProductOptionService _service;
        private readonly IMapper _mapper;

        public ProductOptionsController(ILogger<ProductOptionsController> logger,
            IProductOptionService service,
            IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductOptionViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllProductOptions()
        {
            try
            {
                _logger.LogDebug($"Received {nameof(GetAllProductOptions)} request");
                var productOptions = await _service.GetAllAsync();
                _logger.LogDebug($"Returned {productOptions.Count()} products as part of " +
                                 $"{nameof(GetAllProductOptions)} request");
                return Ok(_mapper.Map<IEnumerable<ProductOptionViewModel>>(productOptions));
            }
            catch (EntityNotFoundException exception)
            {
                _logger.LogError(exception, exception.Message);
                return NotFound(exception.Message);
            }
        }

        [HttpGet]
        [Route("/V{version:apiVersion}/Products/{productId}/Options")]
        [ProducesResponseType(typeof(IEnumerable<ProductOptionViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProductOptionsByProductId(Guid productId)
        {
            try
            {
                _logger.LogDebug($"Received {nameof(GetProductOptionsByProductId)} request with Product Id {productId}");
                var productOptions = await _service.GetAllOptionsByProductIdAsync(productId);
                _logger.LogDebug($"Returned {nameof(GetProductOptionsByProductId)} response with product options " +
                                 $"{JsonConvert.SerializeObject(productOptions)}");

                return Ok(_mapper.Map<IEnumerable<ProductOptionViewModel>>(productOptions));
            }
            catch (ProductOptionNotFoundException exception)
            {
                _logger.LogError(exception, exception.Message);
                return NotFound(exception.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ProductOptionViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProductOptionById(Guid id)
        {
            try
            {
                _logger.LogDebug($"Received {nameof(GetProductOptionById)} request with Id {id}");
                var productOption = await _service.GetByIdAsync(id);
                _logger.LogDebug($"Returned {nameof(GetProductOptionById)} response with product option " +
                                 $"{JsonConvert.SerializeObject(productOption)}");

                return Ok(_mapper.Map<ProductOptionViewModel>(productOption));
            }
            catch (ProductOptionNotFoundException exception)
            {
                _logger.LogError(exception, exception.Message);
                return NotFound(exception.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddProductOption(ProductOptionCreateRequest productOptionCreateRequest)
        {
            _logger.LogDebug($"Received {nameof(AddProductOption)} request with " +
                             $"{JsonConvert.SerializeObject(productOptionCreateRequest)}");
            var productOptionId = await _service.AddAsync(_mapper.Map<ProductOption>(productOptionCreateRequest));
            _logger.LogDebug($"Returned {nameof(AddProductOption)} request with Id {productOptionId}");
            return CreatedAtAction(nameof(AddProductOption), productOptionId, productOptionId);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProductOption(ProductOptionUpdateRequest productOptionUpdateRequest)
        {
            try
            {
                _logger.LogDebug($"Received {nameof(UpdateProductOption)} request with " +
                                 $"{JsonConvert.SerializeObject(productOptionUpdateRequest)}");
                await _service.UpdateAsync(_mapper.Map<ProductOption>(productOptionUpdateRequest));
                _logger.LogDebug($"Returned {nameof(UpdateProductOption)} response for Id {productOptionUpdateRequest.Id}");
                return CreatedAtAction(nameof(UpdateProductOption), productOptionUpdateRequest.Id,
                    productOptionUpdateRequest.Id);
            }
            catch (ProductOptionNotFoundException exception)
            {
                _logger.LogWarning(exception, exception.Message);
                return NotFound(exception.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteProductOption(Guid id)
        {
            try
            {
                _logger.LogDebug($"Received {nameof(DeleteProductOption)} request with Id {id}");
                await _service.DeleteAsync(id);
                _logger.LogDebug($"Returned {nameof(DeleteProductOption)} response with Id {id}");
                return Accepted();
            }
            catch (ProductOptionNotFoundException exception)
            {
                _logger.LogWarning(exception, exception.Message);
                return NotFound(exception.Message);
            }
        }
    }
}