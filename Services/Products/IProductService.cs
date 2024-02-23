using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Payloads.Dtos.Products;
using MVC.Payloads.Requests;
using MVC.Payloads.Requests.Products;

namespace MVC.Services.Products{
    public interface IProductService {
        Task<List<ProductDto>> GetAllProduct(CancellationToken cancellationToken);
        Task<ProductDto> GetAllProductById(int Id);
        Task<JsonResult> GetAllProduct(DataTableParameters dataTableParameters);
        Task<ProductDto> CreateProduct(CreateProduct createProduct, CancellationToken cancellationToken);
        Task<ProductDto> UpdateProduct(int id, UpdateProduct updateProduct, CancellationToken cancellationToken);
        Task<bool> DeleteProduct(int id, CancellationToken cancellationToken);
    }
}