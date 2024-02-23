using AutoMapper;
using MVC.Models;
using MVC.Payloads.Dtos.Products;
using MVC.Payloads.Requests.Products;

namespace MVC.Mappers.Products{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProduct>().ReverseMap();
            CreateMap<ProductDto, CreateProduct>().ReverseMap();
            CreateMap<Product, UpdateProduct>().ReverseMap();
            CreateMap<ProductDto, UpdateProduct>().ReverseMap();
        }
    }
}