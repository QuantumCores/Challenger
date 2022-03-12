using AutoMapper;
using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Challenger.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Challenger.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(
            IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ProductDto> Get(long productId)
        {
            var all = await _productRepository.Get(productId);
            return _mapper.Map<ProductDto>(all);
        }

        [HttpGet("Find")]
        public async Task<ProductDto[]> Find(string search)
        {
            var all = await _productRepository.Find(search);
            return _mapper.Map<ProductDto[]>(all);
        }

        [HttpPost]
        public async Task<ProductDto> Add([FromBody] ProductDto record)
        {
            var entity = _mapper.Map<Product>(record);
            _productRepository.Add(entity);
            await _productRepository.SaveChanges();
            record = _mapper.Map<ProductDto>(entity);

            return record;
        }

        [HttpPatch]
        public async Task<JsonResult> Update([FromBody] ProductDto record)
        {
            await _productRepository.Update(_mapper.Map<Product>(record));
            await _productRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(long id)
        {
            var toDelete = await _productRepository.Get(id);
            _productRepository.Remove(toDelete);
            await _productRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }
    }
}
