using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Models;
using MVC.Payloads.Dtos.Products;
using MVC.Payloads.Requests;
using MVC.Payloads.Requests.Products;
using MVC.Payloads.Responses;
using MVC.Repositories;
using MVC.Utils;

namespace MVC.Services.Products{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper){
            _uow = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ProductDto> CreateProduct(CreateProduct createProduct, CancellationToken cancellationToken)
        {
            Product product = _mapper.Map<Product>(createProduct);
            product.CreatedAt = DateTime.Now;
            _uow.Repository().Add<Product>(product);
            var create = await _uow.CommitAsync(cancellationToken);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int id, CancellationToken cancellationToken)
        {
            var product = await _uow.Repository().GetById<Product>(id);
            if(product is null)
                throw new NotImplementedException();
            _uow.Repository().Delete<Product>(product);
            var update = await _uow.CommitAsync(cancellationToken);
            return true;
        }

        public async Task<ProductDto> UpdateProduct(int id, UpdateProduct updateProduct, CancellationToken cancellationToken)
        {
            var product = await _uow.Repository().GetById<Product>(id);
            if(product is null)
                throw new NotImplementedException();
            product.UpdatedAt = DateTime.Now;
            _uow.Repository().Update<Product>(_mapper.Map<UpdateProduct, Product>(updateProduct, product));
            var update = await _uow.CommitAsync(cancellationToken);
            return _mapper.Map<ProductDto>(product);
        }
        
        public async Task<List<ProductDto>> GetAllProduct(CancellationToken cancellationToken)
        {
            var products = await _uow.Repository().FindAllAsync<Product>(cancellationToken);
            if(products is null)
                throw new NotImplementedException();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<JsonResult> GetAllProduct(DataTableParameters parameters)
        {
            IQueryable<Product> products = _uow.Repository().FindQueryable<Product>();
            var count = products.Count();

            // Filter out on search
            if(!string.IsNullOrWhiteSpace(parameters.search.value))
                products = products.Where(x => !String.IsNullOrEmpty(x.Name) && x.Name.Contains(parameters.search.value));

            var filteredCount = products.Count();

            var ordered = 
                DataTableUtils.Order<Product>(products, parameters, x => x.Name??"Id");
            
            var paged = ordered == null ? new List<Product>() : await ordered.Skip(parameters.start).Take(parameters.length).ToListAsync();

            return new JsonResult(
                new DataTableResult<ProductDto>()
                {
                    draw = parameters.draw,
                    recordsTotal = count,
                    recordsFiltered = filteredCount,
                    data = _mapper.Map<List<ProductDto>>(paged)
                }
            );
        }

        public async Task<ProductDto> GetAllProductById(int Id)
        {
            Product? product = await _uow.Repository().GetById<Product>(Id);
            return _mapper.Map<ProductDto>(product);
        }
    }
}